using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyf.JWTAuthentication
{
    public static class JwtBearerExt
    {
        public static IServiceCollection AddJwtBearerExt(this IServiceCollection services, JWTOptions model)
        {
            //1.Nuget引入程序包：Microsoft.AspNetCore.Authentication.JwtBearer 
            //services.AddAuthentication();//禁用
            services.AddScoped<IJWTService, JWTService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //默认授权机制名称；                                      
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,//是否验证Issuer
                             ValidateAudience = true,//是否验证Audience
                             ValidateLifetime = true,//是否验证失效时间
                             ValidateIssuerSigningKey = true,//是否验证SecurityKey
                             ValidAudience = model.ValidAudience,//Audience
                             ValidIssuer = model.ValidIssuer,//Issuer，这两项和前面签发jwt的设置一致  表示谁签发的Token
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.SecurityKey))//拿到SecurityKey
                             //AudienceValidator = (m, n, z) =>
                             //{
                             //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
                             //},//自定义校验规则，可以新登录后将之前的无效 
                         };
                     });
            return services;
        }
        public static void UseJwt(this IApplicationBuilder app)
        {
            //app.UseAuthentication();
        }
    }

}
