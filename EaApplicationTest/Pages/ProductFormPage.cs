using EaFramework.Driver;
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
    
    private IWebElement NameInput => GetElement(By.Name("Name"));
    private IWebElement DescriptionInput => GetElement(By.Name("Description"));
    private IWebElement PriceInput => GetElement(By.Name("Price"));
    
    private SelectElement ProductTypeSelect
    {
        get
        {
            var element = GetElement(By.Name("ProductType"));
            return new SelectElement(element);
        }
    }

    private void ClickCreate() => GetElement(By.Id("Create")).Submit();
    
    public void CreateProduct(string name, string description, string price, string productType)
    {
        NameInput.SendKeys(name);
        DescriptionInput.SendKeys(description);
        PriceInput.SendKeys(price);
        ProductTypeSelect.SelectByText(productType);
        ClickCreate();
    }
}