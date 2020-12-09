# read me

## solutions
- 1 multi domain
  - 仅使用不同域名登入同一网站
- 2 multi tenant
  - 不同域名不同租户，
    - IdentityServer 中使用 ProfileService，提供 tenant 属性
  - Api 端手动注入 JwtBearOptionsProvider 以验证 Authority 来源
    - 并不在乎 claim 中的 Tenant 属性
- 3 multi tenant finbuckle
  - 不同域名不同租户
  - 使用 finbuckle 授权，代替手动注入 JwtBearOptionsProvider 
- 4 multi tenant finbucke ef
  - 不同域名不同租户
  - 不同数据库
    - 使用 finbuckle 按 tenant 调用数据库