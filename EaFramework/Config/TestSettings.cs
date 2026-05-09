using EaFramework.Models;

namespace EaFramework.Config;

public class TestSettings
{
    public Browser Browser { get; set; }
    public Uri BaseUrl { get; set; }
    public float? TimeoutInterval { get; set; }
    public float? PollingInterval { get; set; }
}