//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using BO;
//namespace PL.PO
//{
//    /// <summary>
//    /// An entity of product in list
//    /// for product list screen and catalog screen of a manager
//    /// </summary>
//    internal class ProductForList : INotifyPropertyChanged
//    {
//        public int Id { get; set; }
//        public string? Name { get; set; }
//        public double Price { get; set; }
//        public eCategory? Category { get; set; }

//        public event PropertyChangedEventHandler? PropertyChanged;

//        public override string ToString() => $@"
//        ID: {Id} | name: {Name} | price: {Price} | category: {Category}";
//    }
//}
