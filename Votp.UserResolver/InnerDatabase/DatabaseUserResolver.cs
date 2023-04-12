using Microsoft.Extensions.DependencyInjection;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Database.Entities;

namespace Votp.UserResolver.InnerDatabase
{
    public class DatabaseUserResolver : IResolver<User>
    {
        IServiceProvider _provider;
        public DatabaseUserResolver(IServiceProvider provider)
        {
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
