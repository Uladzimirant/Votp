using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Database.Entities;

namespace Votp.UserResolver.InnerDatabase
{
    public static class UserResolverServiceCollectonExtentions
    {
        public static IResolverFactoryContainerService<User> RegisterDatabaseUserResolver(this IResolverFactoryContainerService<User> c, IDBLibService dbLibs, IServiceProvider p)
        {
            c.RegisterFactory("Database", new DatabaseUserResolverFactory(p));
            dbLibs.LibAssemblies.Add(typeof(DatabaseUserResolverInfo).Assembly);
            return c;
        }
    }
}
