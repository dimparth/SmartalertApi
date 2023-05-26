using Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySqlConnectionFactory;
using Smartalert.Api.Controllers.Exceptions;
using Smartalert.Api.Implementation;
using Smartalert.Api.Implementation.Repositories;
using Smartalert.Api.Interfaces;
using Smartalert.Api.Interfaces.Repositories;
using System.Text;

namespace Smartalert.Api.DependencyInjection
{
    public static class DIExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IDictionary<string?, string?> connectionStrings)
        {
            services.AddCors();
            services.AddDbFactory(connectionStrings);
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDangerRepository, DangerRepository>();
            services.AddTransient<IDangerService, DangerService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDeveloperService, DeveloperService>();
            services.AddEncryption();
            return services;
        }
        public static void ConfigureExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
