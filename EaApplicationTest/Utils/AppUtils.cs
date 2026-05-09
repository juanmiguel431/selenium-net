using Microsoft.Extensions.Configuration;

namespace EaApplicationTest.Utils;

public class AppUtils
{
    public static IConfigurationRoot LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }
}