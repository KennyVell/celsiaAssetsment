using celsiaAssetsment.Data;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace celsiaAssetsment.Services
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly CelsiaAssetsmentContext _context;
        private readonly IMapper _mapper;
        public InvoicesRepository(CelsiaAssetsmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(InvoiceDTO invoice)
        {
            var nuevoInvoice = _mapper.Map<Invoice>(invoice);
            await _context.Invoices.AddAsync(nuevoInvoice);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                throw new Exception("The invoice doesn't exists.");
            }
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            var invoices = await _context.Invoices.ToListAsync();
            if (invoices.Any()) return invoices;
            return Enumerable.Empty<Invoice>();
        }

        public async Task<Invoice> GetById(int id)
        {
            var invoice = await _context.Invoices.Include(x => x.Client).FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null)
            {
                throw new Exception("he invoice doesn't exists.");
            }
            return invoice;
        }

        public async Task Update(int id, InvoiceDTO invoice)
        {
            var invoiceUpdate = await _context.Invoices.FindAsync(id);
            if (invoiceUpdate == null)
            {
                throw new Exception("he invoice doesn't exists.");
            }
            _mapper.Map(invoice, invoiceUpdate);
            await _context.SaveChangesAsync();
        }
    }
}