using SalesDatePrediction.Data;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class ShippersRepository : IShippersRepository
    {
        private readonly DataContext _context;
        public ShippersRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Shippers> GetShippers()
        {
            return _context.Shippers.OrderBy(sh => sh.ShipperId).ToList();
        }
    }
}
