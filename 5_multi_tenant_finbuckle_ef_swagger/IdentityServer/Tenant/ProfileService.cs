using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Tenant
{
    public class ProfileService : IProfileService
    {
        // private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        // private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _context;
        private readonly ILogger<ProfileService> _logger;
        private readonly TestUserStore _users;

        // public ProfileService (IHttpContextAccessor context, UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        public ProfileService (
            IHttpContextAccessor context, 
            ILogger<ProfileService> logger,
            TestUserStore users = null)
        {
            _context = context;
            _logger = logger;
            // _userManager = userManager;
            // _claimsFactory = claimsFactory;
            _users = users ?? new TestUserStore(TestUsers.Users);
        }

        // public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _users.FindBySubjectId(sub);
            var claims = user.Claims.ToList();

            // var title = "###### identity user claims ######";
            // var msg = JsonSerializer.Serialize(claims);
            // Console.WriteLine(title);
            // Console.WriteLine(msg);
            // _logger.LogInformation(title);
            // _logger.LogInformation(msg);
            
            //Add custom claims in the token here
            context.IssuedClaims = claims;
            return Task.CompletedTask;
        }

        // public async Task IsActiveAsync(IsActiveContext context)
        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _users.FindBySubjectId(sub);

            if(context.Caller == "AuthorizeEndpoit")
            {
                // var tenantId = _context.HttpContext.Request.Query["acr_values"].ToString().Replace("tenant:", "");
                var tenantId = _context.HttpContext.Request.Host.Value.Split(".")[0];

                if(user!=null 
                    && !string.IsNullOrEmpty(tenantId)
                    && tenantId == user.Claims.First(c => c.Type == "TenantId").Value)
                {
                    context.IsActive = true;
                } else {
                    context.IsActive = false;
                }
            } else {
                context.IsActive = user != null;
            }
            return Task.CompletedTask;
        }
    }
}