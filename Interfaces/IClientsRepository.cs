using celsiaAssetsment.Models;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Interfaces
{
    public interface IClientsRepository
    {
        Task Add(ClientDTO client);
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(int id);
        Task Update(int id, ClientDTO client);
        Task Delete(int id);
    }
}