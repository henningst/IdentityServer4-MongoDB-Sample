using IdentityServer4.Core.Services.InMemory;
using System.Linq;
using System.Collections.Generic;
using IdentityServer4.Core.Validation;
using IdSvrHost.Models;
using IdSvrHost.Services;

namespace IdSvrHost.UI.Login
{
    public class LoginService
    {
        private readonly IResourceOwnerPasswordValidator _passwordValidator;
        private readonly IRepository _repository;

        public LoginService(IResourceOwnerPasswordValidator passwordValidator, IRepository repository)
        {
            _passwordValidator = passwordValidator;
            _repository = repository;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _repository.ValidatePassword(username, password);
        }

        public MongoDbUser FindByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }
    }
}
