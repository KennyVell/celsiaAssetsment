using celsiaAssetsment.DTOs;
using celsiaAssetsment.Models;

namespace celsiaAssetsment.Interfaces
{
    public interface IAccountRepository
    {
        Task<Admin> Register(AdminDTO admin);
        Task<Admin> Login(LoginDTO admin);
    }
}