
using Microsoft.Extensions.DependencyInjection;
using HealthChecks.UI;

namespace SecondProject.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("FitnessPlannerCS"), tags: new[] { "database" });
            builder.Services.AddHealthChecksUI().AddSqlServerStorage(builder.Configuration.GetConnectionString("FitnessPlannerCS"));


        }
    }
}
