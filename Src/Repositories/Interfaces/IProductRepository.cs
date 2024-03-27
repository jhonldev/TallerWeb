using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}