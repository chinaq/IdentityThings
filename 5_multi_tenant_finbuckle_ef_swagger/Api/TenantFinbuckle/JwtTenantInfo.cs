using Finbuckle.MultiTenant;

namespace Api.TenantFinbuckle
{
    public class JwtTenantInfo : TenantInfo
    {
        public string JwtAuthority { get; set; }
    }
}