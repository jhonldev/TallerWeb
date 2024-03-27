using TallerWeb.Src.Models;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TallerWeb.Src.Repositories.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}