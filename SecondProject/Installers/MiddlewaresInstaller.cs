
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecondProject.Middlewares;
using System.Text;

namespace SecondProject.Installers
{
    public class MiddlewaresInstaller : IInstaller
    {

        public void InstallServices(WebApplicationBuilder builder)
        {
            try
            {
                builder.Services.AddTransient<ExceptionHandlingMiddleware>();
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience= true,
                        ValidateLifetime= true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                    });
                builder.Services.AddCors();
                builder.Services.AddResponseCaching();


            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
    }
}
