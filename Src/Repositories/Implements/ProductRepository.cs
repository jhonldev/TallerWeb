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

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> ExistingProduct(string name, string type)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Nombre == name && x.Tipo == type);
            if(product != null)
            {
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

            existingProduct.Nombre = productUpdateDto.Nombre != string.Empty ? productUpdateDto.Nombre : existingProduct.Nombre;
            existingProduct.Tipo = productUpdateDto.Tipo != string.Empty ? productUpdateDto.Tipo : existingProduct.Tipo;
            existingProduct.Precio = productUpdateDto.Precio != 0 ? productUpdateDto.Precio : existingProduct.Precio;
            existingProduct.CantidadEnStock = productUpdateDto.CantidadEnStock != 0 ? productUpdateDto.CantidadEnStock : existingProduct.CantidadEnStock;
            existingProduct.Image = productUpdateDto.Image != string.Empty ? productUpdateDto.Image : existingProduct.Image;

            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}