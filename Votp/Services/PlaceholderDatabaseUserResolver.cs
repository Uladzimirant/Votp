//using Votp.Contracts;
//using Votp.Contracts.Services.UserResolver;
//using Votp.DS.Entities;

//namespace Votp.Services
//{
//    public class PlaceholderDatabaseUserResolver : IResolver<User>
//    {
//        private IVotpDbContext _db;
//        public PlaceholderDatabaseUserResolver(IVotpDbContext db)
//        {
//            _db = db;
//        }

//        public IEnumerable<User> GetResolvedList()
//        {
//            return _db.Users.ToList();
//        }
//    }
//}
