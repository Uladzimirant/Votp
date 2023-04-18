using Votp.DS.Entities;
using Votp.Contracts.Services.UserResolver;
using Votp.Contracts.Services;

namespace Votp.UserResolver.Ldap
{
    public static class LdapResolverServiceCollectonExtentions
    {
        public static IResolverFactoryContainerService<User> RegisterLdapUserResolver(this IResolverFactoryContainerService<User> c)
        {
            c.RegisterFactory("Ldap", new LdapUserResolverFactory());
            return c;
        }
    }
}
