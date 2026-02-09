using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using projectWeb.Infrastructure.Data;

namespace projectWeb.Infrastructure.Repositories;

public class UserRepository :GeneralRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public  async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.email == email);
    }
}