using TallerWeb.Src.DTOs.Receipt;

namespace TallerWeb.Src.Service.Interfaces{
    public interface IReceiptService
    {
    
        Task<IEnumerable<ReceiptDto>> GetReceipts();

        Task<IEnumerable<ReceiptDto>> GetReceiptsByClient(int id);
    }
}