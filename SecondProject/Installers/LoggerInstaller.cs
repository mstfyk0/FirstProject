
using Serilog;

namespace SecondProject.Installers
{
    public class LoggerInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            try
            {
                using var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

                builder.Logging.ClearProviders();
                builder.Logging.AddSerilog();
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }

        }
    }
}
