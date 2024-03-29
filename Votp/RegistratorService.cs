﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Votp.Contracts.Services;

namespace Votp
{
    public class RegistratorService : IDBLibService
    {
        public ICollection<Assembly> LibAssemblies { get; set; } = new List<Assembly>();
    }
}
