using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:8000");

        driver.FindElement(By.LinkText("Product")).Click();
        
        driver.FindElement(By.LinkText("Create")).Click();

        driver.FindElement(By.Name("Name")).SendKeys("Product 1");
        driver.FindElement(By.Name("Description")).SendKeys("Description 1");
        driver.FindElement(By.Name("Price")).SendKeys("100");

        var select = new SelectElement(driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        driver.FindElement(By.Id("Create")).Submit();
    }
}