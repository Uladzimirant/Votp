using Votp.DS.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Security;

namespace Votp.UserResolver.Ldap
{
    public class LdapUserResolverInfo : ResolverInfo
    {

        public string Server { get; set; }
        public int? Port { get; set; }

        public string? ConnectionLogin { get; set; }
        public string? ConnectionPassword { get; set; }
    }
}
