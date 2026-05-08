using OpenQA.Selenium;

namespace EaFramework.Driver;

public interface IDriverFixture : IDisposable
{
    IWebDriver Driver { get; }
    void NavigateToUrl(string url);
}