namespace DefaultNamespace;

public interface IUserRepository : IGeneral<User>
{
    Task<User?> GetByEmailAsync(string email);
}