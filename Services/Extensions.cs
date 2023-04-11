using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Todo.Service.Config;

namespace Todo.Service.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfigSection = config.GetSection(nameof(JwtConfig));
            services.Configure<JwtConfig>(jwtConfigSection);
            services.AddSingleton<JwtTokenService>();
            services.AddSingleton<SecurityService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {   
                    var jwtConfig = jwtConfigSection.Get<JwtConfig>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                    };
                    
                });


            return services;
        }
    }
}