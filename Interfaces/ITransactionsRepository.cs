using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Interfaces
{
    public interface ITransactionsRepository
    {
        Task Add(TransactionDTO transaction);
        Task<IEnumerable<Transaction>> GetAll();
        Task<Transaction> GetById(int id);
        Task Update(int id, TransactionDTO transaction);
        Task Delete(int id);
    }
}