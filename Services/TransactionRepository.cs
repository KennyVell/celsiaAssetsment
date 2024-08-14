using celsiaAssetsment.Data;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace celsiaAssetsment.Services
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly CelsiaAssetsmentContext _context;
        private readonly IMapper _mapper;
        public TransactionsRepository(CelsiaAssetsmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(TransactionDTO transaction)
        {
            var nuevoTransaction = _mapper.Map<Transaction>(transaction);
            await _context.Transactions.AddAsync(nuevoTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                throw new Exception("The transaction doesn't exists.");
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            var transactions = await _context.Transactions.ToListAsync();
            if (transactions.Any()) return transactions;
            return Enumerable.Empty<Transaction>();
        }

        public async Task<Transaction> GetById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                throw new Exception("he transaction doesn't exists.");
            }
            return transaction;
        }

        public async Task Update(int id, TransactionDTO transaction)
        {
            var transactionUpdate = await _context.Transactions.FindAsync(id);
            if (transactionUpdate == null)
            {
                throw new Exception("he transaction doesn't exists.");
            }
            _mapper.Map(transaction, transactionUpdate);
            await _context.SaveChangesAsync();
        }
    }
}