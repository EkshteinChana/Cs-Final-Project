using System.Windows;

namespace PL.PO
{
    /// <summary>
    /// An entity of product in list
    /// for product list screen and catalog screen of a manager
    /// </summary>
    internal class ProductForList : DependencyObject
    {
        public static readonly DependencyProperty idProperty = DependencyProperty.Register("Id", typeof(int), typeof(ProductForList), new UIPropertyMetadata(0));
        public static readonly DependencyProperty nameProperty = DependencyProperty.Register("Name", typeof(string), typeof(ProductForList) , new UIPropertyMetadata(""));
        public static readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(double), typeof(ProductForList) ,new UIPropertyMetadata(0.0));
        public static readonly DependencyProperty categoryProperty = DependencyProperty.Register("Category", typeof(BO.eCategory), typeof(ProductForList), new UIPropertyMetadata(BO.eCategory.Others));
        public int Id {
            get { return (int)GetValue(idProperty); }
            set { SetValue(idProperty, value); } 
        }
         public string? Name {
            get { return (string)GetValue(nameProperty); }
            set { SetValue(nameProperty, value ); }  
        }
        public double Price {
            get { return (double)GetValue(priceProperty); }
            set { SetValue(priceProperty, value); }
        }
        public BO.eCategory Category {
            get { return (BO.eCategory)GetValue(categoryProperty); }
            set { SetValue(categoryProperty, value); }
        }             
        public override string ToString() => $@"
        ID: {Id} | name: {Name} | price: {Price} | category: {Category}";
    }
}
