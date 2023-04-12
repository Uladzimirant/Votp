using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database.Entities;

namespace Votp.UserResolver.Ldap
{
    public class LdapUserResolverFactory : IResolverFactory<User>
    {
        public IResolver<User> CreateResolver(ResolverInfo info)
        {
            var fullInfo = info as LdapUserResolverInfo ??
                throw new ArgumentException(
                    $"Wrong ResolverInfo derived class has been passed: {typeof(LdapUserResolverInfo).AssemblyQualifiedName} required, {info.GetType().AssemblyQualifiedName} received",
                    nameof(info));


            return new LdapUserResolver(
                new LdapDirectoryIdentifier(fullInfo.Server, fullInfo.Port ?? 389),
                new System.Net.NetworkCredential(fullInfo.ConnectionLogin, fullInfo.ConnectionPassword));
        }
    }
}
