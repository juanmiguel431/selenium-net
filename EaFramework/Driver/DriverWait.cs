using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaFramework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverManager _driverManager;
    private readonly TestSettings _settings;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public DriverWait(IDriverManager driverManager, TestSettings settings)
    {
        _driverManager = driverManager;
        _settings = settings;
        _webDriverWait = new Lazy<WebDriverWait>(GetDriverWait);
    }

    private WebDriverWait GetDriverWait()
    {
        var timeout = TimeSpan.FromMilliseconds(_settings.TimeoutInterval ?? 500);
        return new WebDriverWait(_driverManager.Driver, timeout)
        {
            PollingInterval = TimeSpan.FromMilliseconds(_settings.PollingInterval ?? 500)
        };
    }
    
    public IWebElement FindElement(By by)
    {
        return _webDriverWait.Value.Until(d => d.FindElement(by));
    }
    
    public IEnumerable<IWebElement> FindElements(By by)
    {
        return _webDriverWait.Value.Until(d => d.FindElements(by));
    }
}