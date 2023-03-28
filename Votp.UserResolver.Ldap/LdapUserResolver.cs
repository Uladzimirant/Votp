using System.DirectoryServices.Protocols;
using System.Net;
using System.Security;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database.Entities;

namespace Votp.UserResolver.Ldap
{
    public class LdapUserResolver : IResolver<User>
    {
        private LdapConnection _connection;

        private LdapDirectoryIdentifier _ldapAddress;
        private NetworkCredential _credential;

        public LdapUserResolver(LdapDirectoryIdentifier address, NetworkCredential credential) {
            _ldapAddress = address;
            _credential = credential;
        }

        public IEnumerable<User> GetResolvedList()
        {
            //string[] servers = new string[1];
            //string user = null;
            //SecureString password = null;

            //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(servers, 1111, true, false);
            //NetworkCredential credential = new NetworkCredential(user,password);
            LdapConnection connection = new LdapConnection(_ldapAddress, _credential);

            connection.Bind();

            string filter = "(objectCategory=users)";
            string[] attributesToReturn = { "uid" };
            string dname = "dc=example,dc=org";
            SearchRequest searchRequest = new SearchRequest(dname, filter, SearchScope.Subtree, attributesToReturn);
            var response = connection.SendRequest(searchRequest) as SearchResponse;

            var result = new List<User>();
            if (response != null) foreach (SearchResultEntry item in response.Entries)
            {
                var u = new User();
                u.Login = (string)item.Attributes["uid"].GetValues(typeof(string)).Single();
                result.Add(u);
            }

            return result;
        }
    }
}