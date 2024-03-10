
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;

namespace SecondProject.Installers
{
    // installer responsible for registration of services connected with Swagger
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {

            try
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(option =>
                {
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",


                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement

                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference= new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string []{}

                        }


                    });
                });
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
    }
}
