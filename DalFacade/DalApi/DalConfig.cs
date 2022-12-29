namespace DalApi;
using System.Xml.Linq;

/// <summary>
/// Extracts information from the Xml file. 
/// </summary>
static class DalConfig
{
    internal static string? s_dalName , s_class , s_namespace;
    /// <summary>
    /// DalConfig ctor
    /// </summary>
    /// <exception cref="DalConfigException"></exception>
    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        var temp = packages?.Where(e => e.Name == s_dalName)
            ?? throw new DalConfigException($"<dal-packages><{s_dalName}><dal-packages> is missing");
        s_class = temp?.Elements("class")?.First()?.Value;
        s_namespace = temp?.Elements("namespace")?.First()?.Value;
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}









