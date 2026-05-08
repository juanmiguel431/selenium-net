

using EaFramework.Driver;
using EaFramework.Extensions;
using OpenQA.Selenium;

namespace EaApplicationTest.Pages;

public class ProductListPage
{
    private readonly IDriverFixture _driver;

    private IWebElement CreateLink => GetElement(By.LinkText("Create"));
    
    private IWebElement Table => GetElement(By.CssSelector(".table"));
    
    
    public ProductListPage(IDriverFixture driver)
    {
        _driver = driver;
    }

    private IWebElement GetElement(By by)
    {
        return _driver.Driver.FindElement(by);
    }
    
    public void ClickCreate() => CreateLink.Click();
    
    public void PerformClickOnSpecialValues(string name, string operation)
    {
        Table.PerformActionOnCell(5, "Name", name, operation);
    }
}