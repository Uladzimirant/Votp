using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations
{
    public class UserResolverService : IUserResolverService
    {

        private readonly ILogger _l;
        private readonly IResolverFactoryContainerService<User> _factoryContainer;
        private readonly IVotpDbContext _db;
        private readonly List<IResolver<User>> _resolvers = new List<IResolver<User>>();

        public UserResolverService(
            ILogger<UserResolverService> l,
            IResolverFactoryContainerService<User> urfc,
            IVotpDbContext db
            )
        {
            _l = l;
            _factoryContainer = urfc;
            _db = db;
            FillResolvers();
        }
        private void FillResolvers() {
            //_resolvers.AddRange(
            //    _db.Resolvers.Select(o => _factoryContainer.GetFactory(o.ResolverType).CreateResolver(null))
            //    );
            foreach (var r in _db.Resolvers)
            {
                var f = _factoryContainer.GetFactory(r.Type);
                var res = f.CreateResolver(null);
                _resolvers.Add(res);
            }
        }

        public ICollection<IResolver<User>> Resolvers
        {
            get { return _resolvers; }
        }

        public void AddResolver(ResolverInfo info)
        {
            var factory = _factoryContainer.GetFactory(info.Type.ToString());
            var res = factory.CreateResolver(null);
            _resolvers.Add(res);
            _db.Resolvers.Add(info);
            _db.SaveChanges();
        }

        public IEnumerable<User> GetUsers()
        {
            return _resolvers.Aggregate(
                Enumerable.Empty<User>(),
                (users, resolver) => users.Concat(resolver.GetResolvedList())
                );
        }
    }
}
