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

        public void CreateToken(TokenIDto dto)
        {
            try
            {
                var user = _db.Users.Single(u => u.Login == dto.UserName);
                var t = _mapper.Map<Token>(dto);
                t.User = user;
                _db.Tokens.Add(t);
                _db.SaveChanges();
            }
            catch (InvalidOperationException) { throw new Exception($"No or multiple user {dto.UserName} found"); }
        }

        public void DeleteTokens(IEnumerable<int> ids)
        {
            _db.Tokens.RemoveRange(_db.Tokens.Where(token => ids.Contains(token.Id)));
            _db.SaveChanges();
        }

        public void DisableTokens(IEnumerable<int> ids)
        {
            return;
        }

        public void EnableTokens(IEnumerable<int> ids)
        {
            return;
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
