using SalesDatePrediction.Data;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {

        private readonly DataContext _context;
        public EmployeesRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Employees> GetEmployees()
        {
            return _context.Employees.OrderBy(or => or.EmpId).ToList();
        }
    }
}
