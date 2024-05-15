using TallerWeb.Src.Models;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.DTOs.Product;

namespace TallerWeb.Src.Repositories.Implements
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly DataContext _context;

        public ReceiptRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Receipt> GenerateReceipt(Receipt receipt)
        {
            await _context.Receipts.AddAsync(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<IEnumerable<Receipt>> GetReceipts()
        {
            var receipts = await _context.Receipts.ToListAsync();
            return receipts;
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsByClient(int id)
        {
            var receipts = await _context.Receipts.Where(x => x.IdUser == id).ToListAsync();
            return receipts;
        }
    }
}