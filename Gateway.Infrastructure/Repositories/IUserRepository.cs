using System.Collections.Generic;
using System.Threading.Tasks;
using Gateway.Domain.Entities;

namespace Gateway.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
