using EaApplicationTest.Models;
using EaFramework.Driver;
using EaFramework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaApplicationTest.Pages;

public class ProductFormPage
{
    private readonly IDriverFixture _driver;

    public ProductFormPage(IDriverFixture driver)
    {
        _driver = driver;
    }
    
    private IWebElement GetElement(By by)
    {
        return _driver.Driver.FindElement(by);
    }
    
    private IWebElement NameElement => GetElement(By.Name("Name"));
    private IWebElement DescriptionElement => GetElement(By.Name("Description"));
    private IWebElement PriceElement => GetElement(By.Name("Price"));
    private IWebElement ProductTypeElement => GetElement(By.Name("ProductType"));
    

    private void ClickCreate() => GetElement(By.Id("Create")).Submit();
    
    public void CreateProduct(string name, string description, string price, string productType)
    {
        NameElement.SendKeys(name);
        DescriptionElement.SendKeys(description);
        PriceElement.SendKeys(price);
        ProductTypeElement.SelectDropdownByText(productType);
        
        ClickCreate();
    }

    public void CreateProduct(Product product)
    {
        CreateProduct(product.Name, product.Description, product.Price.ToString(), product.ProductType.ToString());
    }
}