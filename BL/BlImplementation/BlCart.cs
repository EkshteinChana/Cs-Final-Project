using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal Dal = new DalList();
    public BO.Cart CreateProdInCart(BO.Cart cart, int Id)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidID();
            }
            bool exist = false;
            int inStock;
            foreach (BO.OrderItem i in cart.Items)
            {
                if (i.ProductId == Id)
                {
                    exist = true;
                    inStock = Dal.product.Read(Id).InStock;
                    if (inStock <= 0)
                    {
                        throw new OutOfStock();
                    }
                    i.Amount += 1;
                    i.TotalPrice += i.Price;
                    cart.TotalPrice += i.Price;
                }

            }
            if (exist == true)
            {
                return cart;
            }
            DO.Product dP = Dal.product.Read(Id);
            if (dP.InStock <= 0)
            {
                throw new OutOfStock();
            }
            BO.OrderItem oI = new BO.OrderItem();
            oI.Id = Config.MaxOrderItemId ;
            oI.ProductId = dP.Id;
            oI.Name = dP.Name;
            oI.Price = dP.Price;
            oI.Amount = 1;
            oI.TotalPrice = dP.Price;
            cart.Items.Append(oI);
            cart.TotalPrice += dP.Price;
            return cart;
        }
        catch (IdNotExist exc)
        {
            throw new DataError(exc);
        }
    }

    public void MakeOrder(Cart cart, string customerName, string customerEmail, string customerAddress)
    {

    }

    public Cart UpdateAmountOfProd(Cart cart, int id, int amount)
    {

    }
}
