using AutoFixture.Xunit3;
using EaApplicationTest.Models;
using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.Driver;
using EaFramework.Models;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest;

public class UnitTest1
{
    private readonly TestSettings _settings;

    public UnitTest1(IOptions<TestSettings> options)
    {
        _settings = options.Value;
    }
    
    [Fact]
    public void Test1()
    {
        using var driverManager = new DriverManager(Browser.Chrome, _settings);
        
        var driverWait = new DriverWait(driverManager, _settings);
        
        var homePage = new HomePage(driverWait);
        var productListPage = new ProductListPage(driverWait);
        var productFormPage = new ProductFormPage(driverWait);
        
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
    [InlineData(Browser.Chrome, "Product 3", "Description 3", "3000", "CPU")]
    [InlineData(Browser.Firefox, "Product 4", "Description 4", "4000", "CPU")]
    [InlineData(Browser.Edge, "Product 5", "Description 5", "5000", "CPU")]
    public void Test2(Browser browser, string name, string description, string price, string productType)
    {
        using var driverManager = new DriverManager(browser, _settings);
        
        var driverWait = new DriverWait(driverManager, _settings);
        
        driverWait.FindElement(By.LinkText("Product")).Click();
        
        driverWait.FindElement(By.LinkText("Create")).Click();

        driverWait.FindElement(By.Name("Name")).SendKeys(name);
        driverWait.FindElement(By.Name("Description")).SendKeys(description);
        driverWait.FindElement(By.Name("Price")).SendKeys(price);

        var select = new SelectElement(driverWait.FindElement(By.Name("ProductType")));
        select.SelectByText(productType);
        
        driverManager.Driver.FindElement(By.Id("Create")).Submit();
    }
    
    [Theory]
    [AutoData]
    public void Test3(Product product)
    {
        using var driverManager = new DriverManager(Browser.Chrome, _settings);
        
        var driverWait = new DriverWait(driverManager, _settings);
        
        var homePage = new HomePage(driverWait);
        var productListPage = new ProductListPage(driverWait);
        var productFormPage = new ProductFormPage(driverWait);
        
        homePage.ClickProduct();
        productListPage.ClickCreate();

        productFormPage.CreateProduct(product);
        
        productListPage.PerformClickOnSpecialValues(product.Name, "Details");
    }
}