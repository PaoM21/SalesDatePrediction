using SalesDatePrediction.Models;

namespace SalesDatePrediction.Interfaces
{
    public interface IEmployeesRepository
    {
        ICollection<Employees> GetEmployees();
    }
}
