using TallerWeb.Src.Models;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.DTOs.Product;

namespace TallerWeb.Src.Repositories.Implements
{
    /// <summary>
    /// Represents a repository for managing products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Una colección de productos.</returns>
        public async Task<IEnumerable<Product>> GetProducts(string query)
        {
            if(query == ""){
                return await _context.Products.ToListAsync();
            }
            var products = await _context.Products.Where(u => u.Name.Contains(query) || u.Type.Contains(query) || u.Price.ToString().Contains(query) || u.QuantityStock.ToString().Contains(query)).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsAvailable()
        {

            var products = await _context.Products.Where(x => x.QuantityStock > 0).ToListAsync();
            return products;
        }

        public async Task<bool> CreateProduct(Product product, string Image, string ImageId)
        {
            product.Image = Image;
            product.IdImage = ImageId;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistingProduct(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name && x.Type == type);
            if(product != null){
                return true;
            }   
            return false;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null){
                return false;
            }

            existingProduct.Name = productUpdateDto.Name ?? existingProduct.Name;
            existingProduct.Type = productUpdateDto.Type ?? existingProduct.Type;
            existingProduct.Price = productUpdateDto.Price ?? existingProduct.Price;
            existingProduct.QuantityStock = productUpdateDto.QuantityStock ?? existingProduct.QuantityStock;
            existingProduct.Image = productUpdateDto.Image ?? existingProduct.Image;

            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null){
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BuyProduct(ProductBuyDto productBuyDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == productBuyDto.Name && x.Type == productBuyDto.Type);

            if(product == null || product.QuantityStock < productBuyDto.QuantityStock){
                return -1;
            }

            product.QuantityStock -= productBuyDto.QuantityStock;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> GetTypeProduct(string type)
        {
            var typeProduct = await _context.TypeProducts.FirstOrDefaultAsync(x => x.NameTypeProduct == type);

            if(typeProduct == null){
                return false;
            }
            return true;
        }

        public async Task<int> GetStock(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name && x.Type == type);
            if(product == null){
                return 0;
            }
            return product.QuantityStock;
        }
        public async Task<int> PriceProduct(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name && x.Type == type);
            if(product == null){
                return 0;
            }
            return product.Price;
        }

    }
}