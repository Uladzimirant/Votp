using Votp.DS.Entities;

namespace Votp.Contracts.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
    }
}
