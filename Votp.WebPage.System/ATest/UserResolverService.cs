namespace Votp.ATest
{
    public class UserResolverService : IUserResolverService
    {

        private readonly ILogger _l;
        private readonly UserResolverList _resolvers = new UserResolverList();

        public UserResolverService(ILogger<UserResolverList> l) {
            _l = l;
        }

        public ICollection<IResolver<User>> Resolvers { 
            get { return _resolvers; }
        }

        public IEnumerable<User> GetUsers()
        {
            return _resolvers.ListResolved();
        }
    }
}
