using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Database.Entities;

namespace Votp.Services.DatabaseUserResolver
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
