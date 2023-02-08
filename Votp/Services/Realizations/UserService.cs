using AutoMapper;
using Votp.Models.Response;
using Votp.Services.Contracts;
using Votp.Services.Contracts.UserResolver;

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

        public async Task<List<UserODto>> GetUsers()
        {
            return _userResolverService.GetUsers().Select(_mapper.Map<UserODto>).ToList();
        }
    }
}
