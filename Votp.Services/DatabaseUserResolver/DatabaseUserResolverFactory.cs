using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Database.Entities;

namespace Votp.Services.DatabaseUserResolver
{
    public class DatabaseUserResolverFactory : IResolverFactory<User>
    {
        private IServiceProvider _provider;

        public DatabaseUserResolverFactory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IResolver<User> CreateResolver(IResolverInfo<User> info)
        {
            return new DatabaseUserResolver(_provider);
        }
    }
}
