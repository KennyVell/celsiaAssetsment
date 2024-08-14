using Microsoft.EntityFrameworkCore;
using celsiaAssetsment.Data;
using celsiaAssetsment.DTOs;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.Models;

namespace celsiaAssetsment.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CelsiaAssetsmentContext _context;

        public AccountRepository(CelsiaAssetsmentContext context)
        {
            _context = context;
        }

        public async Task<Admin> Login(LoginDTO admin)
        {
            // Search for the admin by email
            Admin? adminFind = await _context.Admins
                .Where(u => u.Email == admin.Email)
                .FirstOrDefaultAsync();

            // If the admin does not exist, return an exception
            if (adminFind == null)
            {
                throw new Exception("The email does not exist.");
            }

            // Check the admin password
            if (adminFind != null && BCrypt.Net.BCrypt.Verify(admin.Password, adminFind.Password))
            {
                return adminFind;
            }
            else 
            {
                throw new Exception("Incorrect password.");
            }
        }

        public async Task<Admin> Register(AdminDTO admin)
        {
            // Check if the email is already in use
            if (await _context.Admins.AnyAsync(u => u.Email == admin.Email))
            {
                throw new Exception("The email is already in use.");
            }

            // Create a new admin account
            Admin adminRegistration = new Admin
            {
                Name = admin.Name,
                Email = admin.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(admin.Password)
            };

            await _context.Admins.AddAsync(adminRegistration);
            await _context.SaveChangesAsync();
            return adminRegistration;
        }
    }
}