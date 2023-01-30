namespace Votp.ATest
{
    public interface IUserResolverService
    {
        public ICollection<IResolver<User>> Resolvers { get; }
        public void Add(string resolverType);
        public IEnumerable<User> GetUsers();
    }
}
