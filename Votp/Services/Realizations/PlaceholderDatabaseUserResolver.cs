using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations
{
    public class PlaceholderDatabaseUserResolver : IResolver<User>
    {
        private IVotpDbContext _db;
        public PlaceholderDatabaseUserResolver(IVotpDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetResolvedList()
        {
            return _db.Users.ToList();
        }
    }
}
