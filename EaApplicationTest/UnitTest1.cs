using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1
{
    private readonly TestSettings _settings;

    public UnitTest1()
    {
        _settings = new TestSettings
        {
            ApplicationUrl = new Uri("http://localhost:8000"),
            TimeoutInternal = 30
        };
    }
    
    [Fact]
    public void Test1()
    {
        _settings.BrowserType = BrowserType.Chrome;
        using var driver = new DriverFixture(_settings);
        
        var homePage = new HomePage(driver);
        var productListPage = new ProductListPage(driver);
        var productFormPage = new ProductFormPage(driver);
        
        homePage.ClickProduct();
        productListPage.ClickCreate();
        
        productFormPage.CreateProduct("Product 1", "Description 1", "3000", "CPU");
        
        productListPage.PerformClickOnSpecialValues("Product 1", "Details");
    }
    
    [Theory]
    [InlineData(BrowserType.Chrome)]
    [InlineData(BrowserType.Firefox)]
    [InlineData(BrowserType.Edge)]
    public void Test2(BrowserType browserType)
    {
        _settings.BrowserType = browserType;
        using var driver = new DriverFixture(_settings);

        driver.Driver.FindElement(By.LinkText("Product")).Click();
        
        driver.Driver.FindElement(By.LinkText("Create")).Click();

        driver.Driver.FindElement(By.Name("Name")).SendKeys("Product 2");
        driver.Driver.FindElement(By.Name("Description")).SendKeys("Description 2");
        driver.Driver.FindElement(By.Name("Price")).SendKeys("2000");

        var select = new SelectElement(driver.Driver.FindElement(By.Name("ProductType")));
        select.SelectByText("CPU");
        
        driver.Driver.FindElement(By.Id("Create")).Submit();
    }
}