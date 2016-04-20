using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Core.Extensions;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;

namespace IdSvrHost.Services
{
    public class MongoDbProfileService : IProfileService
    {
        private readonly IRepository _repository;

        public MongoDbProfileService(IRepository repository)
        {
            _repository = repository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();

            var user = _repository.GetUserById(subjectId);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtClaimTypes.GivenName, user.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, user.EmailVerified.ToString().ToLower(), ClaimValueTypes.Boolean)
            };

            context.IssuedClaims = claims;

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _repository.GetUserById(context.Subject.GetSubjectId());

            context.IsActive = (user != null) && user.IsActive;
            return Task.FromResult(0);
        }
    }
}
