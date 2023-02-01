using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations.DatabaseUserResolver
{
    public class DatabaseUserResolver : IResolver<User>
    {
        IServiceProvider _provider;
        public DatabaseUserResolver(IServiceProvider provider) {
            _provider = provider;
        }
        public IEnumerable<User> GetResolvedList()
        {
            using (var scope = _provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IVotpDbContext>();
                return db.Users.ToList();
            }   
        }
    }
}
