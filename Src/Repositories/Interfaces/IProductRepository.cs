using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<IEnumerable<Product>> GetProductsAvailable();

        Task<bool> CreateProduct(Product product);

        Task<bool> ExistingProduct(string name, string type);

        Task<bool> UpdateProduct(int id, ProductUpdateDto productDto);

        Task<bool> DeleteProduct(int id);
    }
}