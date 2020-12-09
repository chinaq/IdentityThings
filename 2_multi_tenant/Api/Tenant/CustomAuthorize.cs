using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Tenant
{
    // public class CustomAuthorize : AuthorizeAttribute
    // {
    //     public void OnAuthorization(AuthorizationFilterContext context)
    //     {
    //         Console.WriteLine("On Authorization");
    //         var tenantId = context.HttpContext.User.Claims
    //             .Where(x => x.Type == "TenantId").FirstOrDefault().Value;
    //         var requestUrl = $"{context.HttpContext.Request.Host}";
    //         var requestTenantId = requestUrl.Split('.')[0];
    //         if (tenantId != requestTenantId)
    //         {
    //             context.Result = new UnauthorizedResult();
    //         }

    //         var issuer = context.HttpContext.User
    //             .FindFirst(x => x.Type == "iss").Value;
    //         var issuerTenant = new Uri(issuer).Host.Split('.')[0];
    //         if (issuerTenant != tenantId)
    //         {
    //             context.Result = new UnauthorizedResult();
    //         }
    //         return;
    //     }
    // }
}