using SalesDatePrediction.Models;

namespace SalesDatePrediction.Interfaces
{
    public interface IProductsRepository
    {
        ICollection<Products> GetProducts();
        bool ProductExists(int productId);
    }
}
