using Votp.DS.Database;

namespace Votp.ATest
{
    public class UserResolverService : IUserResolverService
    {

        private readonly ILogger _l;
        private readonly IVotpDbContext _db;
        private readonly UserResolverList _resolvers = new UserResolverList();

        public UserResolverService(ILogger<UserResolverList> l, IVotpDbContext db) {
            _db = db;
            _l = l;
        }

        public ICollection<IResolver<User>> Resolvers { 
            get { return _resolvers; }
        }

        public void Add(string resolverType)
        {
            switch (resolverType)
            {
                case "Database":
                    if (!_resolvers.Any(o => o is PlaceholderDatabaseUserResolver)) {
                        _resolvers.Add(new PlaceholderDatabaseUserResolver(_db));
                    }
                    break;
                default: throw new ArgumentException($"Unknown type of resolver: {resolverType}");
            }
        }

        public IEnumerable<User> GetUsers()
        {
            return _resolvers.ListResolved();
        }
    }
}
