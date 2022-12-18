using System.Xml.Linq;
namespace DalApi;
static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        IEnumerable<XElement> packages = dalConfig?.Element("dal-packages")?.Elements()
           ?? throw new DalConfigException("<dal-packages> element is missing");
        XElement el_class = packages.Elements($"{s_dalName}")?.Elements("class").First()
            ?? throw new DalConfigException($"<{s_dalName}><class/><{s_dalName}> element is missing");
        string s_class = el_class.Value;
        //XElement el_names
        string s_namespace = packages.Elements($"{s_dalName}")?.Elements("namespace").First().Value
            ?? throw new DalConfigException($"<{s_dalName}><namespace/><{s_dalName}> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}


