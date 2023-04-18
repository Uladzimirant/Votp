using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Entities;

namespace Votp.UserResolver.InnerDatabase
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
