# Dev Logs

## 2020.12.18
- 上接前一日，项目 5 尝试仅单一硬编码的 domain 的 swagger 可通过
  - 使用域名校对，不用 `acr_values: tenant: xxx`
  - 已通过
- 过程
  - api & swagger
    - 添加 api 中的 swagger
    - swagger 添加 auth 中间件
  - identity server
    - 添加 swagger 的 client
    - 添加 sagger 的跨域请求
    - 修改验证为比对“域名”和 “user 的 tenant 值”

## 2020.12.17
- 新建项目 5_multi_tenant_finbuckle_ef_swagger
- 计划加入 Swagger 的多租户 IdentityServer4 验证，结果失败
  - 无法动态加载 Swagger 的 Authority Url，已在 github 提问
    - [Is it possible to set the authority url dynamicly based on server address or etc.?](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1932)
  - 当前 IdentityServer 中对 Tenant 的验证，基于请求中的 `acr_values: tenant: xxx` 参数，Swagger 中增加方式亦未知
    - 此方法可以改进为使用 server url 来进行确认，可解决
- Swagger 在 dotnet core 3.x 上的验证
  - 参考 [ASP.NET Core Swagger UI Authorization using IdentityServer4](https://www.scottbrady91.com/Identity-Server/ASPNET-Core-Swagger-UI-Authorization-using-IdentityServer4)