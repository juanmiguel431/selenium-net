using OpenQA.Selenium;

namespace EaFramework.Driver;

public interface IDriverManager : IDisposable
{
    IWebDriver Driver { get; }
    void NavigateToUrl(string url);
}