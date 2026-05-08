using EaFramework.Driver;
using OpenQA.Selenium;

namespace EaApplicationTest.Pages;

public class HomePage
{
    private readonly IDriverFixture _driver;
    private IWebElement LnkHome => GetElementByLinkText("Home");
    private IWebElement LnkPrivacy => GetElementByLinkText("Privacy");
    private IWebElement LnkProduct => GetElementByLinkText("Product");

    public HomePage(IDriverFixture driver)
    {
        _driver = driver;
    }
    
    private IWebElement GetElementByLinkText(string text)
    {
        return _driver.Driver.FindElement(By.LinkText(text));
    }

    public void ClickProduct() => LnkProduct.Click();
}