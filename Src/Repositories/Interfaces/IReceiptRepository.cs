using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.Receipt;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IReceiptRepository
    {
       
        Task<IEnumerable<Receipt>> GetReceipts();

        Task<IEnumerable<Receipt>> GetReceiptsByClient(int id);

        Task<Receipt> GenerateReceipt(Receipt receipt);
    }
}