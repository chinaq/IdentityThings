using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Api.Tenant
{
    public class JwtOptionsInitializer : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly TenantProvider _tenantProvider;

        public JwtOptionsInitializer(TenantProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            // Console.WriteLine($"Before configure named options");
            var authority = _tenantProvider.GetCurrentTenant();
            options.Authority = authority;
            Console.WriteLine($"configured name - {name}");
            Console.WriteLine($"configured authority - {options.Authority}");
            Console.WriteLine($"configured authority hash - {options.Authority.GetHashCode()}");
        }

        public void Configure(JwtBearerOptions options)
            => Debug.Fail("This infrastructure method shouldn't be called.");
    }
}