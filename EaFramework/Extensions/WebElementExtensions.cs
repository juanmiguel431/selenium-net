using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaFramework.Extensions;

public static class WebElementExtensions
{
    public static void SelectDropdownByText(this IWebElement element, string text)
    {
        var select = new SelectElement(element);
        select.SelectByText(text);
    }
    
    public static void SelectDropdownByValue(this IWebElement element, string value)
    {
        var select = new SelectElement(element);
        select.SelectByValue(value);
    }
    
    public static void SelectDropdownByIndex(this IWebElement element, int index)
    {
        var select = new SelectElement(element);
        select.SelectByIndex(index);
    }
}