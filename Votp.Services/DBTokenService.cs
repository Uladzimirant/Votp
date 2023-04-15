using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Votp.Contracts;
using Votp.Contracts.Services;
using Votp.DS.Database;
using Votp.DS.Entities;

namespace Votp.Services
{
    public class DBTokenService : BaseDBService, ITokenService, IUserService
    {
        public DBTokenService(ILogger<DBTokenService> l, IMapper m, IVotpDbContext db) : base(l, m, db)
        {
        }

        public async Task CreateToken(Token input)
        {
            try
            {
                var user = _db.Users.Single(u => u.Login == input.User.Login);
                var t = _mapper.Map<Token>(input);
                t.User = user;
                _db.Tokens.Add(t);
                await _db.AsContext().SaveChangesAsync();
            }
            catch (InvalidOperationException) { throw new Exception($"No or multiple user {input.User.Login} found"); }
        }

        public async Task CreateUser(User input)
        {
            var u = new User() { Login = input.Login };
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

        public async Task<List<Token>> GetTokens()
        {
            return (await _db.Tokens.Include(o => o.User).ToListAsync()).ToList();
        }

        public async Task<List<User>> GetUsers()
        {
            return (await _db.Users.ToListAsync()).ToList();
        }
    }
}
