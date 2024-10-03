using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DataContext _context;
        public CustomersRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderPrediction> GetCustomerOrderPredictions()
        {
            var lastOrderDates = _context.Orders
                .GroupBy(o => o.CustId)
                .Select(g => new
                {
                    CustId = g.Key,
                    LastOrderDate = g.Max(o => o.OrderDate)
                });

            var previousOrders = _context.Orders
                .GroupBy(o => o.CustId)
                .SelectMany(g => g.OrderBy(o => o.OrderDate)
                .Select((order, index) => new
                {
                    CustId = g.Key,
                    OrderDate = order.OrderDate,
                    PreviousOrderDate = index > default(int) ? g.OrderBy(o => o.OrderDate).ElementAt(index - 1).OrderDate : (DateTime?)null
                }))
                .ToList();

            var averageDays = previousOrders
                .Where(o => o.PreviousOrderDate.HasValue)
                .GroupBy(o => o.CustId)
                .Select(g => new
                {
                    CustId = g.Key,
                    PromedioDias = g.Average(o => EF.Functions.DateDiffDay(o.PreviousOrderDate.Value, o.OrderDate))
                });

            var predictions = from lastOrder in lastOrderDates
                              join avgDays in averageDays on lastOrder.CustId equals avgDays.CustId
                              join customer in _context.Customers on lastOrder.CustId equals customer.CustId
                              select new OrderPrediction
                              {
                                  CompanyName = customer.CompanyName,
                                  LastOrderDate = lastOrder.LastOrderDate,
                                  NextPredictedOrder = lastOrder.LastOrderDate.AddDays(avgDays.PromedioDias)
                              };

            return predictions.OrderBy(p => p.CompanyName);
        }
    }
}
