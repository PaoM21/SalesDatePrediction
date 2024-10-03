using SalesDatePrediction.Dto;
using SalesDatePrediction.Models;
using System.Net;

namespace SalesDatePrediction.Interfaces
{
    public interface IOrdersRepository
    {
        ICollection<Orders> GetOrdersByCustom(int custId);
        int CreateOrderWithProduct(OrderProductDto orderProduct);
        bool OrderExists(int orderId);
        bool CustomerExists(int custId);
    }
}
