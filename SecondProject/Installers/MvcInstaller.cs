
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Infrastructure;
using Application;

namespace SecondProject.Installers
{
    // installer responsible for registration of services connected with layer of application and infrastructure

    public class MvcInstaller : IInstaller
    {


        public void InstallServices(WebApplicationBuilder builder)
        {

            try
            {
                builder.Services.AddControllers()
                    .AddOData(options => options.EnableQueryFeatures())
                    ;

                builder.Services.AddApiVersioning(
                    x =>
                    {
                        x.DefaultApiVersion = new ApiVersion(1, 0);
                        x.AssumeDefaultVersionWhenUnspecified = true;
                        x.ReportApiVersions = true;
                        x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                    });
                builder.Services.AddMvc();
                builder.Services.AddInfrastructure();
                builder.Services.AddApplication();
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
    }
}
