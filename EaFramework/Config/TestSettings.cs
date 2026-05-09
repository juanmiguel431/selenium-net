using EaFramework.Models;

namespace EaFramework.Config;

public class TestSettings
{
    public Browser Browser { get; set; }
    public Uri BaseUrl { get; set; }
    public float? TimeoutInterval { get; set; }
    public float? PollingInterval { get; set; }
    public RunType RunType { get; set; }
    public Uri GridUrl { get; set; }
}

public enum RunType
{
    Local,
    Grid
}