using BlApi;
using DalApi;
using Dal;


namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal Dal=new DalList();
    public Cart CreateProdInCart(Cart cart, int Id)
    {
        
    }

    public void MakeOrder(Cart cart, string customerName, string customerEmail, string customerAddress)
    {
        
    }

    public Cart UpdateAmountOfProd(Cart cart, int id, int amount)
    {
        
    }
}
