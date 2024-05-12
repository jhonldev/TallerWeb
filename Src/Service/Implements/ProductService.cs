using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.Repositories.Interfaces;

namespace TallerWeb.Src.Service.Implements
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly MapperService _mapperService;

        public ProductService(IProductRepository productRepository, MapperService mapperService)
        {
            _productRepository = productRepository;
            _mapperService =  mapperService;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var mappedProducts = new List<ProductDto>();
            for (int i = 0; i < products.Count(); i++){
                var productDto = _mapperService.ProductToProductDto(products.ElementAt(i));
                mappedProducts.Add(productDto);
            }
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAvailable()
        {
            var productsAvailable = await _productRepository.GetProductsAvailable();
            var mappedProductsAvailable = new List<ProductDto>();
            for(int i = 0; i < productsAvailable.Count(); i++){
                var productDto = _mapperService.ProductToProductDto(productsAvailable.ElementAt(i));
                mappedProductsAvailable.Add(productDto);
            }
            return mappedProductsAvailable;
        }

        public async Task<bool> CreateProduct(ProductDto productDto)
        {
            if(await _productRepository.ExistingProduct(productDto.Nombre, productDto.Tipo)){
                return false;
            }
            var product = _mapperService.ProductDtoToProduct(productDto);
            var createdProduct = await _productRepository.CreateProduct(product);
            return createdProduct;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var updatedProduct = await _productRepository.UpdateProduct(id, productUpdateDto);
            return updatedProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var deletedProduct = await _productRepository.DeleteProduct(id);
            return deletedProduct;
        }
    }

}