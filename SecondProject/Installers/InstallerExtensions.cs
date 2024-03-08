namespace SecondProject.Installers
{
    public static class InstallerExtensions
    {

        // class responsible for registration of services using each installer
        public static void InstallServiceInAssembly (this WebApplicationBuilder webApplicationBuilder)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where (x=> typeof(IInstaller).IsAssignableFrom (x) && !x.IsInterface && !x.IsAbstract  ).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installers => installers.InstallServices(webApplicationBuilder));        
        
        }

    }
}
