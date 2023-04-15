using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts.Services;
using Votp.DS.TToken;
using Votp.Web.TToken.Controllers;

namespace Votp.Web.TToken
{
    public static class RegistrationExtention
    {
        public static IServiceCollection AddTimedToken(this IServiceCollection service,
            ICollection<Assembly> assembliesList, ICollection<Type> mapperOptList, IDBLibService tokenLibConcreteObject)
        {
            assembliesList.Add(typeof(SystemTimeTokenController).Assembly);
            mapperOptList.Add(typeof(TimedTokenMapperProfile));
            tokenLibConcreteObject.LibAssemblies.Add(typeof(TimeToken).Assembly);
            return service;
        }
    }
}
