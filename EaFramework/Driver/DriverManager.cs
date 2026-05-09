using EaFramework.Config;
using EaFramework.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EaFramework.Driver;

public class DriverManager : IDriverManager
{
    public IWebDriver Driver { get; }
    
    public DriverManager(Browser browser, TestSettings settings)
    {
        Driver = GetDriver(browser);
        Driver.Navigate().GoToUrl(settings.BaseUrl);
    }
    
    private static IWebDriver GetDriver(Browser browser)
    {
        return browser switch
        {
            Browser.Chrome => new ChromeDriver(),
            Browser.Firefox => new FirefoxDriver(),
            Browser.Edge => new EdgeDriver(),
            _ => throw new ArgumentOutOfRangeException(nameof(browser))
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