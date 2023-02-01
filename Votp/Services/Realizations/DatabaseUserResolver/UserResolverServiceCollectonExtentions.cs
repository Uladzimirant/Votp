using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations.DatabaseUserResolver
{
    public static class UserResolverServiceCollectonExtentions
    {
        public static IResolverFactoryContainerService<User> RegisterDatabaseUserResolver(this IResolverFactoryContainerService<User> c, IServiceProvider p)
        {
            c.RegisterFactory("Database", new DatabaseUserResolverFactory(p));
            return c;
        }
    }
}
