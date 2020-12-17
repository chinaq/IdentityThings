// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Data;
using System;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("identity")]
    [Authorize]
    // [CustomAuthorize(AuthenticationSchemes = "Bearer")]
    public class IdentityController : ControllerBase
    {
        private BloggingDbContext context;

        public IdentityController(BloggingDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            var result = context.Blogs.ToList();
            Console.WriteLine(JsonSerializer.Serialize(result));

            return new JsonResult(result);
        }

        // [HttpGet("blogs")]
        // public IActionResult GetBlogs()
        // {
        //     return new JsonResult(context.Blogs);
        // }
    }
}