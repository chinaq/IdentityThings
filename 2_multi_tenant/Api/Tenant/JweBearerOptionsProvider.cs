using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Api.Tenant
{
    public class JweBearerOptionsProvider : IOptionsMonitor<JwtBearerOptions>
    {
        private readonly ConcurrentDictionary<(string name, string tenant), Lazy<JwtBearerOptions>> _cache;
        private readonly IOptionsFactory<JwtBearerOptions> _optionsFactory;
        private readonly TenantProvider _tenantProvider;

        public JweBearerOptionsProvider(
            IOptionsFactory<JwtBearerOptions> optionsFactory,
            TenantProvider tenantProvider)
        {
            _cache = new ConcurrentDictionary<(string, string), Lazy<JwtBearerOptions>>();
            _optionsFactory = optionsFactory;
            _tenantProvider = tenantProvider;
        }

        public JwtBearerOptions CurrentValue => Get(Options.DefaultName);

        public JwtBearerOptions Get(string name)
        {
            // Console.WriteLine($"Before options factory");
            var tenant = _tenantProvider.GetCurrentTenant();
            Lazy<JwtBearerOptions> Create() => new Lazy<JwtBearerOptions>(() => _optionsFactory.Create(name));
            var result = _cache.GetOrAdd((name, tenant), _ => Create()).Value;
            // var result = _optionsFactory.Create(name);
            // Console.WriteLine($"result.ClaimsIssuer = {result.ClaimsIssuer}");
            // Console.WriteLine($"result.Authority = {result.Authority}");
            // result.Authority = tenant;
            Console.WriteLine($"factory name = {name}");
            Console.WriteLine($"factory result.Authority = {result.Authority}");
            Console.WriteLine($"factory result.Authority hash - {result.Authority.GetHashCode()}");
            return result;
        }

        public IDisposable OnChange(Action<JwtBearerOptions, string> listener) => null;
    }
}