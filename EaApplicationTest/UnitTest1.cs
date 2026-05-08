using EaApplicationTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1
{
    private static IWebDriver GetDriverType(BrowserType type)
    {
        return type switch
        {
            BrowserType.Chrome => new ChromeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            BrowserType.Edge => new EdgeDriver(),
            _ => new ChromeDriver()
        };
    }

    [Fact]
    public void Test1()
    {
        using var driver = GetDriverType(BrowserType.Chrome);
        
        driver.Navigate().GoToUrl("http://localhost:8000");

        driver.FindElement(By.LinkText("Product")).Click();
        
        driver.FindElement(By.LinkText("Create")).Click();

        driver.FindElement(By.Name("Name")).SendKeys("Product 1");
        driver.FindElement(By.Name("Description")).SendKeys("Description 1");
        driver.FindElement(By.Name("Price")).SendKeys("1000");

        var select = new SelectElement(driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        driver.FindElement(By.Id("Create")).Submit();
    }
    
    [Theory]
    [InlineData(BrowserType.Chrome)]
    [InlineData(BrowserType.Firefox)]
    [InlineData(BrowserType.Edge)]
    public void Test2(BrowserType browserType)
    {
        using var driver = GetDriverType(browserType);
        
        driver.Navigate().GoToUrl("http://localhost:8000");

        driver.FindElement(By.LinkText("Product")).Click();
        
        driver.FindElement(By.LinkText("Create")).Click();

        driver.FindElement(By.Name("Name")).SendKeys("Product 2");
        driver.FindElement(By.Name("Description")).SendKeys("Description 2");
        driver.FindElement(By.Name("Price")).SendKeys("2000");

        var select = new SelectElement(driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        driver.FindElement(By.Id("Create")).Submit();
    }
}