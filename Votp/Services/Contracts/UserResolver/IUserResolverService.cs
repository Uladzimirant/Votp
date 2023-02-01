
using Votp.DS.Database.Entities;

namespace Votp.Services.Contracts.UserResolver
{
    public interface IUserResolverService
    {
        public ICollection<IResolver<User>> Resolvers { get; }
        public void AddResolver(ResolverInfo resolverType);
        public IEnumerable<User> GetUsers();
    }
}
