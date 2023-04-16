using Votp.DS.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Security;

namespace Votp.UserResolver.Ldap
{
    public class LdapUserResolverInfo : ResolverInfo
    {

        public string Host { get; set; }
        public int? Port { get; set; }

        public string Filter { get; set; }
        public string AttributesJson { get; set; }
        public string Domain { get; set; }

        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
