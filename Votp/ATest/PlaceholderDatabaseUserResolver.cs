using Votp.DS.Database;

namespace Votp.ATest
{
    public class PlaceholderDatabaseUserResolver : IResolver<User>
    {
        private IVotpDbContext _db;
        public PlaceholderDatabaseUserResolver(IVotpDbContext db) { 
            _db = db;
        }

        public IEnumerable<User> ListResolved()
        {
            return _db.Users.Select(o => new Votp.ATest.User { Name = o.Login });
        }
    }
}
