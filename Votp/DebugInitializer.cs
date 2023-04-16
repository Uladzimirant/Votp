using Votp.Contracts;
using Votp.DS.Database;
using Votp.DS.Entities;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Totp.Entities;
using Votp.Utils;

namespace Votp
{
    public static class DebugInitializer
    {
        public static void InitializeDatabases(IServiceProvider serviceProvider)
        {

            using (var dbMain = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IVotpDbContext>() as VotpDbContext)
            {
                using (var dbUsers = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IInnerUsersDBContext>() as InnerUsersDBContext)
                {
                    dbMain.Database.EnsureDeleted();
                    dbMain.Database.EnsureCreated();
                    dbUsers.Database.EnsureDeleted();
                    dbUsers.Database.EnsureCreated();
                    {
                        var r = Randomizer.Instance;
                        var users = Enumerable.Range(0, 5)
                            .Select(i => new User()
                            {
                                Login = r.NextWord(5),
                            }
                            ).ToList();
                        dbUsers.Users!.AddRange(users);
                        dbUsers.SaveChanges();

                        var tokens = new List<Token>();
                        foreach (var user in users) 
                        {
                            tokens.AddRange(Enumerable.Range(1, r.Next(1, 3))
                                    .Select(j => j == 1 ?
                                    new TimeToken() { UserName = user.Login, RegistrationTime = DateTime.Now, Prefix = "1111" } :
                                    new TotpToken() { UserName = user.Login, RegistrationTime = DateTime.Now, Prefix = "1111", Key = Convert.FromBase64String("VGhlIHF1aWNrIGJyb3duIGZveCA=") } as Token)
                                    .ToList());
                        }
                        dbMain.Tokens.AddRange(tokens);
                        dbMain.Resolvers.Add(new ResolverInfo() { ResolverName = "Database" });
                        //db.Resolvers.Add(new LdapUserResolverInfo() { ResolverName = "Ldap", Server = "localhost", Port = 10389, ConnectionLogin = "cn=admin,dc=example,dc=org", ConnectionPassword = "admin" }) ;

                        dbMain.SaveChanges();
                    }
                }
            }
        }
    }
}
