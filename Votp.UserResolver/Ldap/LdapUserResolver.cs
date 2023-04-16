using System.DirectoryServices.Protocols;
using System.Net;
using System.Security;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Entities;

namespace Votp.UserResolver.Ldap
{
    public class LdapUserResolver : IResolver<User>
    {
        //private LdapConnection _connection;

        private LdapDirectoryIdentifier _ldapAddress;
        private NetworkCredential _credential;

        private string _filter = "(objectClass=inetOrgPerson)";
        private string[] _attributesToReturn = { "uid" };
        private string _dname = "dc=example,dc=org";

        public LdapDirectoryIdentifier LDAPAddress { get => _ldapAddress; }

        public LdapUserResolver(LdapDirectoryIdentifier address, NetworkCredential credential)
        {
            _ldapAddress = address;
            _credential = credential;
        }

        public LdapUserResolver(LdapDirectoryIdentifier address, NetworkCredential credential, string filter, string attributesMap, string domainName)
        {
            _ldapAddress = address;
            _credential = credential;
            _filter = filter;
            _attributesToReturn = new string[] { "uid" }; //todo
            _dname = domainName;
        }

        public IEnumerable<User> GetResolvedList()
        {
            //string[] servers = new string[1];
            //string user = null;
            //SecureString password = null;

            //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(servers, 1111, true, false);
            //NetworkCredential credential = new NetworkCredential(user,password);
            //using (var entry = new DirectoryEntry( "ldap://dc=example,dc=org", "cn=admin,dc=example,dc=org", "admin"))
            //{
            //    var searcher = new DirectorySearcher(entry);
            //    searcher.Filter = "(objectCategory=users)";
            //    searcher.PropertiesToLoad.AddRange(new string[] { "uid" });

            //    var result = new List<User>();
            //    foreach (SearchResult item in searcher.FindAll()) 
            //    {
            //        var u = new User();
            //        u.Login = (string)item.Properties["uid"][0];
            //        result.Add(u);
            //    }
            //    return result;
            //}


            using (LdapConnection connection = new LdapConnection(_ldapAddress, _credential, AuthType.Basic))
            {
                connection.SessionOptions.ProtocolVersion = 3;

                connection.Bind();



                SearchRequest searchRequest = new SearchRequest(_dname, _filter, SearchScope.Subtree, _attributesToReturn);
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
}