using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<OrderPredictionDto> GetCustomerOrderPredictions();
    }
}
