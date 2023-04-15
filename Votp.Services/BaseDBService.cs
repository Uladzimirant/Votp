using AutoMapper;
using Microsoft.Extensions.Logging;
using Votp.Contracts;

namespace Votp.Services
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
