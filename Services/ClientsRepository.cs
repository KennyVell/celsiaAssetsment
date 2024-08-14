using celsiaAssetsment.Data;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace celsiaAssetsment.Services
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly CelsiaAssetsmentContext _context;
        private readonly IMapper _mapper;
        public ClientsRepository(CelsiaAssetsmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(ClientDTO client)
        {
            if (await _context.Clients.AnyAsync(c => c.Email == client.Email))
            {
                throw new Exception("The email is already in use.");
            }

            var nuevoClient = _mapper.Map<Client>(client);
            await _context.Clients.AddAsync(nuevoClient);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                throw new Exception("The client doesn't exists.");
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _context.Clients.ToListAsync();
            if (clients.Any()) return clients;
            return Enumerable.Empty<Client>();
        }

        public async Task<Client> GetById(int id)
        {
            var client = await _context.Clients
                    .Include(c => c.Invoices)
                    .Include(c => c.Transactions)
                    .FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                throw new Exception("he client doesn't exists.");
            }
            return client;
        }

        public async Task Update(int id, ClientDTO client)
        {
            var clientUpdate = await _context.Clients.FindAsync(id);
            if (clientUpdate == null)
            {
                throw new Exception("he client doesn't exists.");
            }
            _mapper.Map(client, clientUpdate);
            await _context.SaveChangesAsync();
        }
    }
}