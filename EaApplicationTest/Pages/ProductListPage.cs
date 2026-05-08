

using EaFramework.Driver;
using OpenQA.Selenium;

namespace EaApplicationTest.Pages;

public class ProductListPage
{
    private readonly IDriverFixture _driver;

    private IWebElement CreateLink => GetElement(By.LinkText("Create"));
    
    
    public ProductListPage(IDriverFixture driver)
    {
        _driver = driver;
    }

    private IWebElement GetElement(By by)
    {
        return _driver.Driver.FindElement(by);
    }
    
    public void ClickCreate() => CreateLink.Click();
}