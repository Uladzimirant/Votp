using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Votp.Contracts;
using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Entities;
using Votp.Exceptions;

namespace Votp.Services
{
    public class DBTokenService : ITokenService //, IUserService
    {
        private readonly IUserService _userService;
        private readonly ILogger<DBTokenService> _logger;
        private readonly IMapper _mapper;
        private readonly IVotpDbContext _db;

        public DBTokenService(ILogger<DBTokenService> logger, IMapper mapper, IVotpDbContext db, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _db = db;
            _userService = userService;
        }

        public async Task CreateToken(Token input)
        {
            var users = (await _userService.GetUsers()).FindAll(u => u.Login == input.UserName);
            if (users.Count == 0) throw new ExpectedException($"No user '{input.UserName}' found");
            if (users.Count > 1) throw new ExpectedException($"Multiple users '{input.UserName}' found. Remove users with same name");
            _db.Tokens.Add(input);
            await _db.AsContext().SaveChangesAsync();
        }

        //public async Task CreateUser(User input)
        //{
        //    var u = new User() { Login = input.Login };
        //    _db.Users.Add(u);
        //    await _db.AsContext().SaveChangesAsync();
        //}
        //public async Task DeleteUsers(IEnumerable<int> ids)
        //{
        //    _db.Users.RemoveRange(_db.Users.Where(u => ids.Contains(u.Id)));
        //    await _db.AsContext().SaveChangesAsync();
        //}

        public async Task DeleteTokens(IEnumerable<int> ids)
        {
            _db.Tokens.RemoveRange(_db.Tokens.Where(token => ids.Contains(token.Id)));
            await _db.AsContext().SaveChangesAsync();
        }

        public async Task DisableTokens(IEnumerable<int> ids)
        {
            await _db.Tokens.Where(t => ids.Contains(t.Id)).ExecuteUpdateAsync(e => e.SetProperty(t => t.Locked, v => true));
            return;
        }

        public async Task EnableTokens(IEnumerable<int> ids)
        {
            await _db.Tokens.Where(t => ids.Contains(t.Id)).ExecuteUpdateAsync(e => e.SetProperty(t => t.Locked, v => false));
            return;
        }

        public async Task<List<Token>> GetTokens()
        {
            var tokenTask = _db.Tokens.ToListAsync();
            var userTask = _userService.GetUsers();
            await Task.WhenAll(tokenTask, userTask);
            var tokens = tokenTask.Result;
            var users = userTask.Result;
            tokens.ForEach(t => t.User = users.SingleOrDefault(u => u.Login == t.UserName, null));
            return tokens;
        }

        //public async Task<List<User>> GetUsers()
        //{
        //    return (await _db.Users.ToListAsync()).ToList();
        //}
    }
}
