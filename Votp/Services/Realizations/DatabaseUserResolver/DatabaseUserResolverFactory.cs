using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations.DatabaseUserResolver
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
