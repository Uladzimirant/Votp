namespace Votp.ATest
{
    public class UserResolverList : List<IResolver<User>>, IResolver<User>
    {
        public UserResolverList()
        {
        }

        public UserResolverList(IEnumerable<IResolver<User>> collection) : base(collection)
        {
        }

        public UserResolverList(int capacity) : base(capacity)
        {
        }

        public IEnumerable<User> ListResolved()
        {
            return this.Aggregate(Enumerable.Empty<User>(), (accumulation, r) => accumulation.Concat(r.ListResolved())).ToList();
        }
    }
}
