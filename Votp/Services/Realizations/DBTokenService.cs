using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Votp.DS.Database;
using Votp.Models.Response;
using Votp.Services.Contracts;

namespace Votp.Services.Realizations
{
    public class DBTokenService : ITokenService
    {
        private ILogger<DBTokenService> _l;
        private IMapper _mapper;
        private IVotpDbContext _db;

        private List<TokenODto> _tokens;
        private List<UserODto> _users;
        public DBTokenService(ILogger<DBTokenService> l, IMapper m, IVotpDbContext db)
        {
            _l = l;
            _mapper = m;
            _db = db;
        }

        public List<TokenODto> GetTokens()
        {
            return _db.Tokens.Include(o => o.User).Select(_mapper.Map<TokenODto>).ToList();
        }

        public List<UserODto> GetUsers()
        {
            return _db.Users.Select(_mapper.Map<UserODto>).ToList();
        }
    }
}
