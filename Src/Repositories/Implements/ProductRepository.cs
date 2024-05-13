using TallerWeb.Src.Models;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.DTOs.Product;

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

        public async Task<IEnumerable<Product>> GetProductsAvailable()
        {
            var products = await _context.Products.Where(x => x.QuantityStock > 0).ToListAsync();
            return products;
        }

        public async Task<bool> CreateProduct(Product product)
        {
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

        public async Task<int> GetStock(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name && x.Type == type);
            if(product == null){
                return 0;
            }
            return product.QuantityStock;
        }

        public async Task<Receipt> GenerateReceipt(Receipt receipt)
        {
            await _context.Receipts.AddAsync(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<int> PriceProduct(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name && x.Type == type);
            if(product == null){
                return 0;
            }
            return product.Price;
        }

        public async Task<IEnumerable<Receipt>> GetReceipts()
        {
            var receipts = await _context.Receipts.ToListAsync();
            return receipts;
        }
    }
}