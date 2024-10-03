using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Interfaces;

namespace SalesDatePrediction.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DataContext _context;
        public CustomersRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderPredictionDto> GetCustomerOrderPredictions()
        {
            //var lastOrderDates = _context.Orders
            //    .GroupBy(o => o.CustId)
            //    .Select(g => new
            //    {
            //        CustId = g.Key,
            //        LastOrderDate = g.Max(o => o.OrderDate)
            //    });

            //var previousOrders = _context.Orders
            //    .GroupBy(o => o.CustId)
            //    .SelectMany(g => g.OrderBy(o => o.OrderDate)
            //    .Select((order, index) => new
            //    {
            //        CustId = g.Key,
            //        OrderDate = order.OrderDate,
            //        PreviousOrderDate = index > default(int) ? g.OrderBy(o => o.OrderDate).ElementAt(index - 1).OrderDate : (DateTime?)null
            //    }))
            //    .ToList();

            //var averageDays = previousOrders
            //    .Where(o => o.PreviousOrderDate.HasValue)
            //    .GroupBy(o => o.CustId)
            //    .Select(g => new
            //    {
            //        CustId = g.Key,
            //        PromedioDias = g.Average(o => EF.Functions.DateDiffDay(o.PreviousOrderDate.Value, o.OrderDate))
            //    });

            //var predictions = from lastOrder in lastOrderDates
            //                  join avgDays in averageDays on lastOrder.CustId equals avgDays.CustId
            //                  join customer in _context.Customers on lastOrder.CustId equals customer.CustId
            //                  select new OrderPrediction
            //                  {
            //                      CompanyName = customer.CompanyName,
            //                      LastOrderDate = lastOrder.LastOrderDate,
            //                      NextPredictedOrder = lastOrder.LastOrderDate.AddDays(avgDays.PromedioDias)
            //                  };

            //return predictions.OrderBy(p => p.CompanyName);

            var query = @"
                WITH CTE0 AS (
                    SELECT MAX(orderdate) AS LastOrderDate, custid, orderdate
                    FROM Sales.Orders
                    GROUP BY custid, orderdate
                ), 
                CTE1 AS (
                    SELECT
                        custid,
                        LastOrderDate,
                        LAG(orderdate) OVER (PARTITION BY custid ORDER BY orderdate) AS FechaAnterior,
                        ROW_NUMBER() OVER (PARTITION BY custid ORDER BY LastOrderDate DESC) AS RN
                    FROM CTE0
                ), 
                CTE2 AS (
                    SELECT
                        c.custid,
                        AVG(DATEDIFF(DAY, c.FechaAnterior, c.LastOrderDate)) AS PromedioDias
                    FROM CTE1 c
                    WHERE c.FechaAnterior IS NOT NULL
                    GROUP BY c.custid
                )
                SELECT 
                    Cu.companyname,
                    c1.LastOrderDate,
                    DATEADD(DAY, c2.PromedioDias, c1.LastOrderDate) AS NextPredictedOrder
                FROM CTE1 c1
                JOIN CTE2 c2 ON c1.custid = c2.custid
                JOIN Sales.Customers AS Cu ON c1.custid = Cu.custid
                WHERE c1.RN = 1
                ORDER BY companyname;";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var predictions = new List<OrderPredictionDto>();
                    while (result.Read())
                    {
                        predictions.Add(new OrderPredictionDto
                        {
                            CompanyName = result.GetString(0),
                            LastOrderDate = result.GetDateTime(1),
                            NextPredictedOrder = result.GetDateTime(2)
                        });
                    }
                    return predictions;
                }
            }
        }
    }
}
