using SalesDatePrediction.Data;
using SalesDatePrediction.Interfaces;
using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _context;
        public ProductsRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Products> GetProducts()
        {
            return _context.Products.OrderBy(pr => pr.ProductId).ToList();
        }
    }
}
