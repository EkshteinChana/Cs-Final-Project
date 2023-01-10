using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;
namespace PL.PO
{
    /// <summary>
    /// An entity of product in list
    /// for product list screen and catalog screen of a manager
    /// </summary>
    internal class ProductForList : DependencyObject
    {
        public static readonly DependencyProperty idDepenProp = DependencyProperty.Register("Id", typeof(int), typeof(ProductForList));
        public static readonly DependencyProperty nameDependProp = DependencyProperty.Register("Name", typeof(string), typeof(ProductForList));
        public static readonly DependencyProperty priceDependProp = DependencyProperty.Register("Price", typeof(double), typeof(ProductForList));

        public int Id {
            get { return (int)GetValue(idDepenProp); }
            set { SetValue(idDepenProp, value); } 
        }

        public string? Name {
            get { return }
            set { SetValue( id, value ); }  
        }
        public double Price { get; set; }
        public eCategory? Category { get; set; }

        
       
        public override string ToString() => $@"
        ID: {Id} | name: {Name} | price: {Price} | category: {Category}";
    }
}
