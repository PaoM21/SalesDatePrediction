using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
using SalesDatePrediction.Dto;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _context;
        public OrdersRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Orders> GetOrders()
        {
            return _context.Orders.OrderBy(or => or.OrderId).ToList();
        }

        public ICollection<Orders> GetOrdersByCustom(int custId)
        {
            return _context.Orders.Where(or => or.CustId == custId).ToList();
        }

        public int CreateOrderWithProduct(OrderProductDto orderProduct)
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct), "Order product cannot be null.");
            }

            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            var orderIdParameter = new SqlParameter
            {
                ParameterName = "@OrderId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            try
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC AddNewOrder @Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry, @Productid, @Unitprice, @Qty, @Discount, @OrderId OUT",
                    new SqlParameter("@Empid", orderProduct.EmpId),
                    new SqlParameter("@Shipperid", orderProduct.ShipperId),
                    new SqlParameter("@Shipname", orderProduct.ShipName),
                    new SqlParameter("@Shipaddress", orderProduct.ShipAddress),
                    new SqlParameter("@Shipcity", orderProduct.ShipCity),
                    new SqlParameter("@Orderdate", orderProduct.OrderDate),
                    new SqlParameter("@Requireddate", orderProduct.RequiredDate),
                    new SqlParameter("@Shippeddate", orderProduct.ShippedDate),
                    new SqlParameter("@Freight", orderProduct.Freight),
                    new SqlParameter("@Shipcountry", orderProduct.ShipCountry),
                    new SqlParameter("@Productid", orderProduct.ProductId),
                    new SqlParameter("@Unitprice", orderProduct.UnitPrice),
                    new SqlParameter("@Qty", orderProduct.Qty),
                    new SqlParameter("@Discount", orderProduct.Discount),
                    orderIdParameter
                );


                int orderId = orderIdParameter.Value is DBNull ? default(int) : (int)orderIdParameter.Value;

                return orderId;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the order.", ex);
            }
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(or => or.OrderId == orderId);
        }

        public bool CustomerExists(int custId)
        {
            return _context.Orders.Any(or => or.CustId == custId);
        }
    }
}
