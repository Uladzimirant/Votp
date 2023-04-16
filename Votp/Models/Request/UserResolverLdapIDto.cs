using System.ComponentModel.DataAnnotations;

namespace Votp.Models.Request
{
    public class UserResolverLdapIDto
    {
        public string Name { get; set; } = "Ldap";
        public string Host { get; set; }
        [Range(0, 65536)]
        public int Port { get; set; }
        public string Filter { get; set; }
        public string Attributes { get; set; }
        public string Domain { get; set; }
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
