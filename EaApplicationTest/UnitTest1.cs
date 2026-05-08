using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1 : IDisposable
{
    private readonly ChromeDriver _driver;

    public UnitTest1()
    {
        _driver = new ChromeDriver();
    }
    
    [Fact]
    public void Test1()
    {
        _driver.Navigate().GoToUrl("http://localhost:8000");

        _driver.FindElement(By.LinkText("Product")).Click();
        
        _driver.FindElement(By.LinkText("Create")).Click();

        _driver.FindElement(By.Name("Name")).SendKeys("Product 1");
        _driver.FindElement(By.Name("Description")).SendKeys("Description 1");
        _driver.FindElement(By.Name("Price")).SendKeys("1000");

        var select = new SelectElement(_driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        _driver.FindElement(By.Id("Create")).Submit();
    }
    
    [Fact]
    public void Test2()
    {
        _driver.Navigate().GoToUrl("http://localhost:8000");

        _driver.FindElement(By.LinkText("Product")).Click();
        
        _driver.FindElement(By.LinkText("Create")).Click();

        _driver.FindElement(By.Name("Name")).SendKeys("Product 2");
        _driver.FindElement(By.Name("Description")).SendKeys("Description 2");
        _driver.FindElement(By.Name("Price")).SendKeys("2000");

        var select = new SelectElement(_driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        _driver.FindElement(By.Id("Create")).Submit();
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}