# Dev Logs

## 2020.12.27
- 新建项目 5_multi_tenant_finbuckle_ef_swagger
- 计划加入 Swagger 的多租户 IdentityServer4 验证，结果失败
  - 无法动态加载 Swagger 的 Authority Url，已在 github 提问
    - [Is it possible to set the authority url dynamicly based on server address or etc.?](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1932)
  - 当前 IdentityServer 中对 Tenant 的验证，基于请求中的 `acr_values: tenant: xxx` 参数，Swagger 中增加方式亦未知
    - 此方法可以改进为使用 server url 来进行确认，可解决