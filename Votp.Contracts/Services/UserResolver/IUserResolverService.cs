﻿using Votp.DS.Entities;

namespace Votp.Contracts.Services.UserResolver
{
    public interface IUserResolverService
    {
        public ICollection<IResolver<User>> Resolvers { get; }
        public Task AddResolver(ResolverInfo resolverType);
        public IEnumerable<User> GetUsers();
    }
}
