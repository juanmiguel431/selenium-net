using EaApplicationTest.Models;

namespace EaFramework.Config;

public class TestSettings
{
    public BrowserType BrowserType { get; set; }
    public Uri BaseUrl { get; set; }
    public float? TimeoutInterval { get; set; }
    public float? PollingInterval { get; set; }
}