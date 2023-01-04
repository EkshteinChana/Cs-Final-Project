namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// This class implements the CRUD on the database functions for each product in the store.
/// </summary>
internal class Product : IProduct
{
    /// <summary>
    /// A private help function to convert XElement of Product to DO.Product entity.
    /// </summary>
    private DO.Product convertFromXmlToDoProduct(XElement productXml)
    {
        DO.Product prod = new();
        prod.Id = Convert.ToInt32(productXml.Element("Id").Value.ToString());
        prod.Name = productXml.Element("Name").Value.ToString();
        prod.Price = Convert.ToDouble(productXml.Element("Price").Value.ToString());
        prod.InStock = Convert.ToInt32(productXml.Element("InStock").Value.ToString());
        prod.Category = (eCategory)Enum.Parse(typeof(eCategory), productXml.Element("Category").Value.ToString());
        return prod;
    }

    /// <summary>
    /// A private help function to convert DO.Product entity to XElement of Product.
    /// </summary>
    private XElement convertFromDoProductToXmlProduct(DO.Product prod)
    {
        XElement el = new("Product",
                new XElement("Id", Convert.ToString(prod.Id)),
                new XElement("Name", prod.Name),
                new XElement("Price", Convert.ToDouble(prod.Price)),
                new XElement("InStock", Convert.ToString(prod.InStock)),
                new XElement("Category", Convert.ToString(prod.Category)));
        return el;
    }

    /// <summary>
    /// A function to add a new product to the Xml database.
    /// </summary>
    public int Create(DO.Product prod)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement>? products = root?.Elements("Product").ToList();
        XElement? tmpProduct = products?.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value?.ToString()) == prod.Id)?.FirstOrDefault();
        if (tmpProduct != null)
        {
            throw new IdAlreadyExistsException();
        }
        XElement el = convertFromDoProductToXmlProduct(prod);
        root?.Add(el);
        root?.Save("..\\..\\..\\..\\xml\\product.xml");
        return prod.Id;
    }

    /// <summary>
    /// A function to delete a product from the xml database.
    /// </summary>
    public void Delete(int id)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement> products = root.Elements("Product").ToList();
        XElement tmpProduct = products?.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value?.ToString()) == id).FirstOrDefault();
        if (tmpProduct == null)
        {
            throw new IdNotExistException("product");
        }
        tmpProduct.Remove();
        root?.Save("..\\..\\..\\..\\xml\\product.xml");
    }

    /// <summary>
    ///  A function to get the information about specific product in the xml database by ID.
    /// </summary>
    public DO.Product Read(int id)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement> products = root.Elements("Product").ToList();
        XElement productXml = products?.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value?.ToString()) == id).FirstOrDefault();
        if (productXml == null)
        {
            throw new IdNotExistException("product");
        }
        return convertFromXmlToDoProduct(productXml);
    }

    /// <summary>
    ///  A function to get the information about all the products in the xml database.
    /// </summary>
    public IEnumerable<DO.Product> Read(Func<DO.Product, bool>? func = null)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement>? xmlProducts = root?.Elements("Product").ToList();
       var productList = from xmlProd in xmlProducts
                          select new DO.Product
                          {
                              Id = Convert.ToInt32(xmlProd?.Element("Id")?.Value.ToString()),
                              Name = xmlProd?.Element("Name")?.Value.ToString(),
                              Price = Convert.ToDouble(xmlProd?.Element("Price")?.Value.ToString()),
                              InStock = Convert.ToInt32(xmlProd?.Element("InStock")?.Value.ToString()),
                              Category = (eCategory)Enum.Parse(typeof(eCategory), xmlProd.Element("Category").Value.ToString())
                          };
        return func == null ? productList : productList.Where(func);
    }

    /// <summary>
    /// A function to get a specific product from the xml database by a function.
    /// </summary>
    public DO.Product ReadSingle(Func<DO.Product, bool> func)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement>? xmlProducts = root?.Elements("Product").ToList();
        var productList = from xmlProd in xmlProducts
                          select new DO.Product
                          {
                              Id = Convert.ToInt32(xmlProd?.Element("Id")?.Value.ToString()),
                              Name = xmlProd?.Element("Name")?.Value.ToString(),
                              Price = Convert.ToDouble(xmlProd?.Element("Price")?.Value.ToString()),
                              InStock = Convert.ToInt32(xmlProd?.Element("InStock")?.Value.ToString()),
                              Category = (eCategory)Enum.Parse(typeof(eCategory), xmlProd.Element("Category").Value.ToString())
                          };
        DO.Product prod = productList.Where(func).FirstOrDefault();
        if (prod.Equals(default(DO.Product)))
        {
            throw new ObjectNotExistException("product");
        }
        return prod;
    }

    /// <summary>
    ///  A function to update a specific product in the xml database. 
    /// </summary>
    public void Update(DO.Product product)
    {
        XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\product.xml").Root;
        List<XElement>? xmlProducts = root?.Elements("Product").ToList();
        XElement? xmlProduct = xmlProducts?.Where(prd => Convert.ToInt32(prd.Element("Id")?.Value?.ToString()) == product.Id).FirstOrDefault();
        if (xmlProduct == null)
        {
            throw new IdNotExistException("product");
        }
        xmlProduct.Remove();
        XElement el = convertFromDoProductToXmlProduct(product);
        root?.Add(el);
        root?.Save("..\\..\\..\\..\\xml\\product.xml");
    }
}


