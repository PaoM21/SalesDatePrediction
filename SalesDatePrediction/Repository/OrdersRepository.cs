using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Data;
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

        public ICollection<Orders> GetOrdersByCustom(int custId)
        {
            return _context.Orders.Where(p => p.CustId == custId).ToList();
        }

        public bool CreateOrderWithProduct(
            int empId,
            int shipperId,
            string shipName,
            string shipAdress,
            string shipCity,
            DateTime orderDate,
            DateTime requiredDate,
            DateTime shippedDate,
            Decimal freight,
            string shipCountry,
            int productId,
            Decimal unitPrice,
            int qty,
            Decimal Discount,
            Orders orders)
        {
            var createOrder = _context.Orders
               .FromSqlRaw(
                "EXEC AddNewOrder @Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry, @Productid, @Unitprice, @Qty, @Discount",
                   new SqlParameter("@Empid", empId),
                   new SqlParameter("@Shipperid", shipperId),
                   new SqlParameter("@Shipname", shipName),
                   new SqlParameter("@Shipaddress", shipAdress),
                   new SqlParameter("@Shipcity", shipCity),
                   new SqlParameter("@Orderdate", orderDate),
                   new SqlParameter("@Requireddate", requiredDate),
                   new SqlParameter("@Shippeddate", shippedDate),
                   new SqlParameter("@Freight", freight),
                   new SqlParameter("@Shipcountry", shipCountry),
                   new SqlParameter("@Productid", productId),
                   new SqlParameter("@Unitprice", unitPrice),
                   new SqlParameter("@Qty", qty),
                   new SqlParameter("@Discount", Discount))
               .ToList();

            _context.Add(createOrder);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > default(int) ? true : false;
        }
    }
}
