using Votp.DS.Entities;

namespace Votp.Contracts.Services.UserResolver
{
    public interface IUserResolverService
    {
        public ICollection<IResolver<User>> Resolvers { get; }
        public Task<ICollection<ResolverInfo>> GetResolverInfos();
        public Task AddResolver(ResolverInfo resolverType);
        public Task RemoveResolver(int id);
        public IEnumerable<User> GetUsers();
    }
}
