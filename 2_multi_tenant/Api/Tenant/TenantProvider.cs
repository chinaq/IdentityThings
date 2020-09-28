using System;
using Microsoft.AspNetCore.Http;

namespace Api.Tenant
{
    public class TenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public string GetCurrentTenant()
        {
            string requestUrl = $"{this._httpContextAccessor.HttpContext.Request.Host}";
            var tenantId = requestUrl.Split('.')[0];
            // string authorityDomain = "lalita.com:5000";
            string authorityDomain = "q.q:5001";
            //string authorityScheme = this._httpContextAccessor.HttpContext.Request.Scheme;
            string authorityScheme = "http";
            string authorityUrl = $"{authorityScheme}://{tenantId}.{authorityDomain}";

            // Console.WriteLine($"authority Url - {authorityUrl}");

            return authorityUrl;
        }
    }
}