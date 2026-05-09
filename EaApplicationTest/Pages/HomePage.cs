using EaFramework.Driver;
using OpenQA.Selenium;

namespace EaApplicationTest.Pages;

public class HomePage
{
    private readonly IDriverWait _driver;
    private IWebElement LnkHome => GetElementByLinkText("Home");
    private IWebElement LnkPrivacy => GetElementByLinkText("Privacy");
    private IWebElement LnkProduct => GetElementByLinkText("Product");

    public HomePage(IDriverWait driver)
    {
        _driver = driver;
    }
    
    private IWebElement GetElementByLinkText(string text)
    {
        return _driver.FindElement(By.LinkText(text));
    }

    public void ClickProduct() => LnkProduct.Click();
}