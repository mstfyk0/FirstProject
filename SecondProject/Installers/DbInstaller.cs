
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SecondProject.Installers
{
    public class DbInstaller : IInstaller 
    {
        // installer responsible for choosing specific sql database using DbContext class and connection string
        public void InstallServices(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddDbContext<FitnessPlannerContext>( optionsAction => optionsAction.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("FitnessPlannerCS")));

        }

    }
}
