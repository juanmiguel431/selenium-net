using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaFramework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverFixture _driverFixture;
    private readonly TestSettings _settings;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public DriverWait(IDriverFixture driverFixture, TestSettings settings)
    {
        _driverFixture = driverFixture;
        _settings = settings;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
    }

    private WebDriverWait GetWaitDriver()
    {
        var timeout = TimeSpan.FromMilliseconds(_settings.TimeoutInterval ?? 500);
        return new WebDriverWait(_driverFixture.Driver, timeout)
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