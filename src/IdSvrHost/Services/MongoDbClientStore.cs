using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc.ActionConstraints;

namespace IdSvrHost.Services
{
    public class MongoDbClientStore : IClientStore
    {
        private readonly IRepository _repository;

        public MongoDbClientStore(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _repository.GetClient(clientId);
            if (client == null)
            {
                return Task.FromResult<Client>(null);
            }

            return Task.FromResult(new Client()
            {
                ClientId = client.ClientId,
                Flow = client.Flow,
                AllowedScopes = client.AllowedScopes,
                RedirectUris = client.RedirectUris,
                ClientSecrets = client.ClientSecrets.Select(s => new Secret(s.Sha256())).ToList()
            });
        }
    }
}
