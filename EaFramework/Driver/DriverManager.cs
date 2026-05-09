using EaFramework.Config;
using EaFramework.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace EaFramework.Driver;

public class DriverManager : IDriverManager
{
    private readonly TestSettings _settings;
    public IWebDriver Driver { get; }
    
    public DriverManager(Browser browser, TestSettings settings)
    {
        _settings = settings;
        Driver = settings.RunType == RunType.Local ? GetDriver(browser) : GetRemoteDriver(browser);
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
    
    private RemoteWebDriver GetRemoteDriver(Browser browser)
    {
        return browser switch
        {
            Browser.Chrome => new RemoteWebDriver(_settings.GridUrl, new ChromeOptions()),
            Browser.Firefox => new RemoteWebDriver(_settings.GridUrl, new FirefoxOptions()),
            Browser.Edge => new RemoteWebDriver(_settings.GridUrl, new EdgeOptions()),
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