using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Tenant
{
    public class ProfileService : IProfileService
    {
        // private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _context;
        private readonly TestUserStore _users;

        // public ProfileService (IHttpContextAccessor context, UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        public ProfileService (IHttpContextAccessor context, TestUserStore users = null)
        {
            _context = context;
            // _userManager = userManager;
            // _claimsFactory = claimsFactory;
            _users = users ?? new TestUserStore(TestUsers.Users);
        }

        // public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            // var user = await _userManager.FindByIdAsync(sub);
            // var principal = await _claimsFactory.CreateAsync(user);
            var user = _users.FindBySubjectId(sub);

            // var claims = principal.Claims.ToList();
            // claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            var claims = user.Claims.ToList();

            //Add custom claims in the token here
            // claims.Add(new Claim("TenantId", user.TenantId ?? string.Empty));
            context.IssuedClaims = claims;
            return Task.CompletedTask;
        }

        // public async Task IsActiveAsync(IsActiveContext context)
        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            // var user = await _userManager.FindByIdAsync(sub);
            var user = _users.FindBySubjectId(sub);

            if(context.Caller == "AuthorizeEndpoit")
            {
                var tenantId = _context.HttpContext.Request.Query["acr_values"].ToString().Replace("tenant:", "");
                // if(user!=null && !string.IsNullOrEmpty(tenantId)&&tenantId==user.TenantId)
                if(user!=null 
                    && !string.IsNullOrEmpty(tenantId)
                    && tenantId == user.Claims.First(c => c.Type == "TenantId").Value)
                {
                    context.IsActive = true;
                }
                else
                {
                    context.IsActive = false;
                }
            }
            else
            {
                context.IsActive = user != null;
            }
            return Task.CompletedTask;
        }
    }
}