using EaApplicationTest.Utils;
using EaFramework.Config;
using Microsoft.Extensions.DependencyInjection;

namespace EaApplicationTest;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = AppUtils.LoadConfiguration();
        
        services.Configure<TestSettings>(configuration.GetSection("TestConfig"));
        
        services.AddSingleton(configuration);
    }
}