using EaApplicationTest.Models;
using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EaFramework.Driver;

public class DriverFixture : IDriverFixture
{
    private readonly TestSettings _settings;
    public IWebDriver Driver { get; }
    
    public DriverFixture(TestSettings settings)
    {
        _settings = settings;
        Driver = GetDriverType(_settings.BrowserType);
        Driver.Navigate().GoToUrl(_settings.ApplicationUrl);
    }

    private static IWebDriver GetDriverType(BrowserType type)
    {
        return type switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            BrowserType.Edge => new EdgeDriver(),
            _ => new ChromeDriver()
        };
    }

    public void NavigateToUrl(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public void Dispose()
    {
        Driver.Dispose();
    }
}