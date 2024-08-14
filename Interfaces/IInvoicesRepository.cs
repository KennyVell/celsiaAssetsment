using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Interfaces
{
    public interface IInvoicesRepository
    {
        Task Add(InvoiceDTO invoice);
        Task<IEnumerable<Invoice>> GetAll();
        Task<Invoice> GetById(int id);
        Task Update(int id, InvoiceDTO invoice);
        Task Delete(int id);
    }
}