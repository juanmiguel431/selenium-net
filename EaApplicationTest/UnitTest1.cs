using AutoFixture.Xunit2;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaApplicationTest.Utils;
using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1
{
    private readonly TestSettings _settings;

    public UnitTest1()
    {
        var configuration = AppUtils.LoadConfiguration();
        _settings = configuration.GetSection("TestConfig").Get<TestSettings>() ?? throw new Exception("appsettings.json is not configured");
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

        var product = new Product
        {
            Name = "Product 1",
            Description = "Description 1",
            Price = 1000,
            ProductType = ProductType.CPU
        };
        
        productFormPage.CreateProduct(product);
        
        productListPage.PerformClickOnSpecialValues(product.Name, "Details");
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
    
    [Theory]
    [AutoData]
    public void Test3(Product product)
    {
        _settings.BrowserType = BrowserType.Chrome;
        using var driver = new DriverFixture(_settings);
        
        var homePage = new HomePage(driver);
        var productListPage = new ProductListPage(driver);
        var productFormPage = new ProductFormPage(driver);
        
        homePage.ClickProduct();
        productListPage.ClickCreate();

        productFormPage.CreateProduct(product);
        
        productListPage.PerformClickOnSpecialValues(product.Name, "Details");
    }
}