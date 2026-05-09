using OpenQA.Selenium;

namespace EaFramework.Driver;

public interface IDriverWait
{
    IWebElement FindElement(By by);
    IEnumerable<IWebElement> FindElements(By by);
}