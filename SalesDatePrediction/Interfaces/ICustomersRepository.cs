using SalesDatePrediction.Models;

namespace SalesDatePrediction.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<OrderPrediction> GetCustomerOrderPredictions();
    }
}
