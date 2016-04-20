using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;

namespace IdSvrHost.Services
{
    public class MongoDbScopeStore : IScopeStore
    {
        public Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            throw new NotImplementedException();
        }
    }
}
