using SalesDatePrediction.Models;
using System.Net;

namespace SalesDatePrediction.Interfaces
{
    public interface IOrdersRepository
    {
        ICollection<Orders> GetOrdersByCustom(int custId);
        bool CreateOrderWithProduct(
            int empId, int shipperId, string shipName, string shipAdress, string shipCity, DateTime orderDate, DateTime requiredDate,
            DateTime shippedDate, Decimal freight, string shipCountry, int productId, Decimal unitPrice, int qty, Decimal Discount, Orders orders);
    }
}
