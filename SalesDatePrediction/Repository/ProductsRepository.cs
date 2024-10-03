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

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(or => or.ProductId == productId);
        }
    }
}
