using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Product;
using TallerWeb.Src.DTOs.Receipt;
using TallerWeb.Src.DTOs.User;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IMapperService
    {
        Product ProductDtoToProduct(ProductDto productDto);

        ProductDto ProductToProductDto(Product product);

        ReceiptDto ReceiptToReceiptDto(Receipt receipt);

        Receipt ReceiptDtoToReceipt(ReceiptDto receiptDto);

        //Uusario
        public User RegisterUserDtoToUser(RegisterUserDto registerUserDto);

        public UserDto UserToUserDto(User user);

    }
}