using System.Threading.Tasks;
using IdentityServer4.Core.Validation;

namespace IdSvrHost.Services
{
    public class MongoDbResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IRepository _repository;

        public MongoDbResourceOwnerPasswordValidator(IRepository repository)
        {
            _repository = repository;
        }

        public Task<CustomGrantValidationResult> ValidateAsync(string userName, string password, ValidatedTokenRequest request)
        {
            if (_repository.ValidatePassword(userName, password))
            {
                return Task.FromResult(new CustomGrantValidationResult(userName, "password"));
            }
       
            return Task.FromResult(new CustomGrantValidationResult("Wrong username or password"));
        }
    }
}
