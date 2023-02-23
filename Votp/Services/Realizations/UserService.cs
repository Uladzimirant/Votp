using AutoMapper;
using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database.Entities;

namespace Votp.Services.Realizations
{
    public class UserService : IUserService
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IMapper _mapper;

        public UserService(IUserResolverService userResolverService, IMapper mapper) {
            _userResolverService = userResolverService;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsers()
        {
            return _userResolverService.GetUsers().ToList();
        }
    }
}
