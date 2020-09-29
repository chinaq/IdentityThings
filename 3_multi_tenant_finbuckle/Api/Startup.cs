// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Finbuckle.MultiTenant;
using Api.TenantFinbuckle;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata=false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        // ValidateIssuer = false,
                    };
                });
            
            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://zero.q.q:5003", "http://one.q.q:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMultiTenant<JwtTenantInfo>()
                .WithConfigurationStore()
                .WithHostStrategy()
                .WithPerTenantOptions<JwtBearerOptions>((o, tenantInfo) =>
                {
                    // Assume tenants are configured with an authority string to use here.
                    o.Authority = tenantInfo.JwtAuthority;
                });

            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddSingleton<TenantProvider>();
            // services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtOptionsInitializer>();
            // services.AddSingleton<IOptionsMonitor<JwtBearerOptions>, JweBearerOptionsProvider>();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseCors("default");
            app.UseMultiTenant();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope");
            });
        }
    }
}
