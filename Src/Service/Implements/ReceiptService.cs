using TallerWeb.Src.DTOs.Receipt;
using TallerWeb.Src.Models;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.Repositories.Interfaces;



namespace TallerWeb.Src.Service.Implements
{

    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;

        private readonly IMapperService _mapperService;

        public ReceiptService(IReceiptRepository receiptRepository, IMapperService mapperService)
        {
            _receiptRepository = receiptRepository;
            _mapperService =  mapperService;
        }


        public async Task<IEnumerable<ReceiptDto>> GetReceipts()
        {
            var receipts = await _receiptRepository.GetReceipts();
            var mappedReceipts = new List<ReceiptDto>();
            for (int i = 0; i < receipts.Count(); i++){
                var receiptDto = _mapperService.ReceiptToReceiptDto(receipts.ElementAt(i));
                mappedReceipts.Add(receiptDto);
            }
            return mappedReceipts;
        }

        public async Task<IEnumerable<ReceiptDto>> GetReceiptsByClient(int id)
        {
            var receipts = await _receiptRepository.GetReceiptsByClient(id);
            var mappedReceipts = new List<ReceiptDto>();
            for (int i = 0; i < receipts.Count(); i++){
                var receiptDto = _mapperService.ReceiptToReceiptDto(receipts.ElementAt(i));
                mappedReceipts.Add(receiptDto);
            }
            return mappedReceipts;
        }
    }
}