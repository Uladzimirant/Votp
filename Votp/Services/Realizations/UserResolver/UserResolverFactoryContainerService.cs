using Votp.DS.Database.Entities;
using Votp.Services.Contracts.UserResolver;

namespace Votp.Services.Realizations.UserResolver
{
    public class UserResolverFactoryContainerService : IResolverFactoryContainerService<User>
    {
        private Dictionary<string, IResolverFactory<User>> factories = new Dictionary<string, IResolverFactory<User>>(); 

        public IResolverFactory<User> GetFactory(string name)
        {
            return factories[name];
        }

        public void RegisterFactory(string uniqueName, IResolverFactory<User> f)
        {
            factories[uniqueName] = f;
        }
    }
}
