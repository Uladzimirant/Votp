using AutoMapper;
using Votp.DS.Database;

namespace Votp.Services.Realizations
{
    public abstract class BaseDBService
    {
        protected ILogger<BaseDBService> _l;
        protected IMapper _mapper;
        protected IVotpDbContext _db;

        public BaseDBService(ILogger<BaseDBService> l, IMapper m, IVotpDbContext db)
        {
            _l = l;
            _mapper = m;
            _db = db;
        }
    }
}
