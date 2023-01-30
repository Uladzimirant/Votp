namespace Votp.ATest
{
    public interface IUserResolverService
    {
        public ICollection<IResolver<User>> Resolvers { get; }
        public IEnumerable<User> GetUsers();
    }
}
