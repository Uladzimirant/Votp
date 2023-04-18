using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Votp.Contracts.Services;
using Votp.Tokens.Time.Controllers;
using Votp.Tokens.Time.Entities;

namespace Votp.Tokens.Time
{
    public static class RegistrationExtention
    {
        public static IServiceCollection AddTimedToken(this IServiceCollection service,
            ICollection<Assembly> assembliesList)
        {
            
            assembliesList.Add(typeof(SystemTimeTokenController).Assembly);
            return service;
        }
    }
}
