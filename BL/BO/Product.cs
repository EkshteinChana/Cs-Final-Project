using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Main logical entity of product 
/// for product detail screens (for manager) 
/// and actions on a product
/// </summary>
public class Product
{
    public int Id { get; set; }//product id
    public string Name { get; set; }
    public double Price { get; set; }
    public eCategory Category { get; set; }
    public int InStock { get; set; }//amount in stock

    public override string ToString() => $@"
        ID: {Id}
        name: {Name} 
        price: {Price}
        category: {Category}
    	amount in stock: {InStock}
        ";

}
