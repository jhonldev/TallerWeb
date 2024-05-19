using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;
using TallerWeb.Src.Models;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.Repositories.Interfaces;


namespace TallerWeb.Src.Service.Implements
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IReceiptRepository _receiptRepository;

        private readonly IMapperService _mapperService;

        private readonly IPhotoService _photoService;

        public ProductService(IProductRepository productRepository, IMapperService mapperService, IReceiptRepository receiptRepository, IPhotoService photoService)
        {
            _productRepository = productRepository;
            _mapperService =  mapperService;
            _receiptRepository = receiptRepository;
            _photoService = photoService;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(string query)
        {
            var products = await _productRepository.GetProducts(query);
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

        public async Task<bool> CreateProduct(ProductDto productDto, IFormFile photo)
        {
            var result = await _photoService.AddPhoto(photo);

            if(productDto.Type == null || result.Error != null){
                return false;
            }

            var typeProduct = await _productRepository.GetTypeProduct(productDto.Type);
            
            if (typeProduct == false){
                return false;
            }

            if(productDto.Name == null || productDto.Type == null || await _productRepository.ExistingProduct(productDto.Name, productDto.Type)){
                return false;
            }
            var product = _mapperService.ProductDtoToProduct(productDto);
            var createdProduct = await _productRepository.CreateProduct(product, result.SecureUrl.AbsoluteUri, result.PublicId);
            return createdProduct;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            if(productUpdateDto.Type == null){
                return false;
            }

            var typeProduct = await _productRepository.GetTypeProduct(productUpdateDto.Type);
            
            if (typeProduct == false){
                return false;
            }

            var updatedProduct = await _productRepository.UpdateProduct(id, productUpdateDto);
            return updatedProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var deletedProduct = await _productRepository.DeleteProduct(id);
            return deletedProduct;
        }

        public async Task<ReceiptDto?> BuyProduct(ProductBuyDto productBuyDto, string userId)
        {
            if(productBuyDto.Type == null){
                return null;
            }

            var typeProduct = await _productRepository.GetTypeProduct(productBuyDto.Type);
            
            if (typeProduct == false){
                return null;
            }

            var result = await _productRepository.BuyProduct(productBuyDto);

            if(result == -1 || productBuyDto.Name == null || productBuyDto.Type == null){
                return null;
            }

            int price = _productRepository.PriceProduct(productBuyDto.Name, productBuyDto.Type).Result;

            Receipt receipt = new Receipt{
                    IdProduct = result,
                    NameProduct = productBuyDto.Name,
                    TypeProduct = productBuyDto.Type,
                    UnitPriceProduct = price,
                    QuantityPruchased = productBuyDto.QuantityStock,
                    PriceFinal = productBuyDto.QuantityStock * price,
                    Date = DateTime.Today,
                    IdUser = int.Parse(userId)
                };
                var receiptGenerate = await _receiptRepository.GenerateReceipt(receipt);
                var mappedReceipt = _mapperService.ReceiptToReceiptDto(receiptGenerate);
                return mappedReceipt;
        }

    }
}