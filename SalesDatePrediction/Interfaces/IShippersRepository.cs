using SalesDatePrediction.Models;

namespace SalesDatePrediction.Interfaces
{
    public interface IShippersRepository
    {
        ICollection<Shippers> GetShippers();
    }
}
