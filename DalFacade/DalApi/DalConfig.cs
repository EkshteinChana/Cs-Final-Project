//namespace DalApi;
//using System.Xml.Linq;

//static class DalConfig
//{
//    internal static string? s_dalName;
//    internal static Dictionary<string, string> s_dalPackages;

//    static DalConfig()
//    {
//        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
//            ?? throw new DalConfigException("dal-config.xml file is not found");
//        s_dalName = dalConfig?.Element("dal")?.Value
//            ?? throw new DalConfigException("<dal> element is missing");
//        var packages = dalConfig?.Element("dal-packages")?.Elements()
//            ?? throw new DalConfigException("<dal-packages> element is missing");
//        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
//    }
//}

//[Serializable]
//public class DalConfigException : Exception
//{
//    public DalConfigException(string msg) : base(msg) { }
//    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
//}






using System.Xml.Linq;
namespace DalApi;
static class DalConfig
{
    internal static string? s_dalName, s_class;
    internal static Dictionary<string, string> s_dalPackages, s_dalInfo;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        //IEnumerable<XElement> el_class = packages.Elements($"{s_dalName}");
        //IEnumerable<XElement> temp = el_class?.Elements("class");
        //List<XElement> temp5 = (List<XElement>)temp;
        //XElement temp2 = temp5?[0]
        //    ?? throw new DalConfigException($"<{s_dalName}><class/><{s_dalName}> element is missing");
        //s_class = temp2.Value;
        //XElement el_namespace = packages.Elements($"{s_dalName}")?.Elements("namespace").First()
        //    ?? throw new DalConfigException($"<{s_dalName}><namespace/><{s_dalName}> element is missing");
        //s_namespace = el_namespace.Value;
        var el_class = packages?.Elements($"{s_dalName}")?.Elements("class").First()
                ?? throw new DalConfigException($"oy!");
        s_class = el_class.Value;

        var dalInfo = packages?.Elements($"{s_dalName}")
          ?? throw new DalConfigException($"<{s_dalName}> element is missing");
        //s_dalInfo = dalInfo.ToDictionary(p => "" + p.Name, p => p.Value);

        //s_class = packages.Descendants().Where(e => e.Name == "");
        //s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}


