using AutoMapper;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services.Contracts;

namespace Votp.Services.Realizations
{
    public class DBTokenService : BaseDBService, ITokenService, IUserService
    {
        public DBTokenService(ILogger<DBTokenService> l, IMapper m, IVotpDbContext db) : base(l, m, db)
        {
        }

        public async Task CreateToken(TokenIDto dto)
        {
            try
            {
                var user = _db.Users.Single(u => u.Login == dto.UserName);
                var t = _mapper.Map<Token>(dto);
                t.User = user;
                _db.Tokens.Add(t);
                await _db.AsContext().SaveChangesAsync();
            }
            catch (InvalidOperationException) { throw new Exception($"No or multiple user {dto.UserName} found"); }
        }

        public async Task CreateUser(UserIDto dto)
        {
            var u = new User() { Login = dto.Name };
            _db.Users.Add(u);
            await _db.AsContext().SaveChangesAsync();
        }
        public async Task DeleteUsers(IEnumerable<int> ids)
        {
            _db.Users.RemoveRange(_db.Users.Where(u => ids.Contains(u.Id)));
            await _db.AsContext().SaveChangesAsync();
        }

        public async Task DeleteTokens(IEnumerable<int> ids)
        {
            _db.Tokens.RemoveRange(_db.Tokens.Where(token => ids.Contains(token.Id)));
            await _db.AsContext().SaveChangesAsync();
        }

        public async Task DisableTokens(IEnumerable<int> ids)
        {
            return;
        }

        public async Task EnableTokens(IEnumerable<int> ids)
        {
            return;
        }

        public async Task<List<TokenODto>> GetTokens()
        {
            return (await _db.Tokens.Include(o => o.User).ToListAsync()).Select(_mapper.Map<TokenODto>).ToList();
        }

        public async Task<List<UserODto>> GetUsers()
        {
            return (await _db.Users.ToListAsync()).Select(_mapper.Map<UserODto>).ToList();
        }
    }
}
