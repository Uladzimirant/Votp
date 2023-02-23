using Votp.DS.Database.Entities;

namespace Votp.Contracts.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
    }
}
