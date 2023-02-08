using Votp.Models.Response;

namespace Votp.Services.Contracts
{
    public interface IUserService
    {
        public Task<List<UserODto>> GetUsers();
    }
}
