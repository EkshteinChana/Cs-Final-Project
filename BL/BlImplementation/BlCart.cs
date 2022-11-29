using BlApi;
using DalApi;
using Dal;

namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal Dal = new DalList();
    public BO.Cart CreateProdInCart(BO.Cart cart, int id)
    {
        try
        {
            if (id < 1)
            {
                throw new InvalidValue("ID");
            }
            bool exist = false;
            int inStock;
            if (cart.Items != null)
                foreach (BO.OrderItem i in cart.Items)
                {
                    if (i.ProductId == id)//The product is already in the shopping cart
                    {
                        exist = true;
                        inStock = Dal.product.Read(id).InStock;
                        if (inStock <= 0)
                        {
                            throw new OutOfStock(0);
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
            //The product is not in the shopping cart
            DO.Product dP = Dal.product.Read(id);
            if (dP.InStock <= 0)
            {
                throw new OutOfStock(0);
            }
            BO.OrderItem oI = new BO.OrderItem();
            oI.Id = Config.MaxCartOrderItemId;
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

    public void MakeOrder(BO.Cart cart, string customerName, string customerEmail, string customerAddress)
    {
        checkDataMakeOrder(cart, customerName, customerEmail, customerAddress);
        DO.Order dOrder = new DO.Order();
        dOrder.CustomerName = customerName;
        dOrder.CustomerEmail = customerEmail;
        dOrder.CustomerAddress = customerAddress;
        dOrder.OrderDate = DateTime.Now;
        dOrder.ShipDate = DateTime.MinValue;
        dOrder.DeliveryDate = DateTime.MinValue;
        bool tryId = true;
        int orderId = 0;
        while (tryId)
        {
            tryId = false;
            try
            {
                dOrder.Id = DataSource.Config.MaxOrderId;
                orderId = Dal.order.Create(dOrder);
            }
            catch (IdAlreadyExists)
            {
                tryId = true;
            }
        }
        foreach (BO.OrderItem item in cart.Items)//Building order item objects in the data layer based on items ordered in the shopping cart
        {
            DO.OrderItem dOrderItem = new DO.OrderItem();
            dOrderItem.ProductId = item.ProductId;
            dOrderItem.OrderId = orderId;
            dOrderItem.Price = item.Price;
            dOrderItem.Amount = item.Amount;
            tryId = true;
            while (tryId)
            {
                tryId = false;
                try
                {
                    dOrderItem.Id = DataSource.Config.MaxOrderItemId;
                    Dal.orderItem.Create(dOrderItem);
                }
                catch (IdAlreadyExists)
                {
                    tryId = true;
                }
            }
        }
        foreach (BO.OrderItem item in cart.Items)//Updating the amount in stock in the data layer of the ordered products
        {
            try
            {
                DO.Product dP = Dal.product.Read(item.ProductId);
                if (dP.InStock < item.Amount)
                {
                    throw new InvalidOrderItem($"The amount you ordered from product: {item.ProductId} is not in stock");
                }
                dP.InStock -= item.Amount;
                Dal.product.Update(dP);
            }
            catch (IdNotExist exc)
            {
                throw new InvalidOrderItem($"There is no product with ID number {item.ProductId}");
            }
        }
    }
    /// <summary>
    /// A private help function to check the correctness of the customer's details and the shopping cart.
    /// </summary>
    private void checkDataMakeOrder(BO.Cart cart, string customerName, string customerEmail, string customerAddress)
    {
        if (string.IsNullOrEmpty(customerName))
        {
            throw new InvalidValue("customer name");
        }
        if (!(checkEmailValidation(customerEmail)))
        {
            throw new InvalidValue("customer email");
        }
        if (string.IsNullOrEmpty(customerAddress))
        {
            throw new InvalidValue("customer address");
        }
        foreach (BO.OrderItem item in cart.Items)
        {
            if (item.Amount < 1)
            {
                throw new InvalidOrderItem($"You took an invalid amount of product: {item.ProductId}");
            }
            try
            {
                DO.Product dP = Dal.product.Read(item.ProductId);
                if (dP.InStock < item.Amount)
                {
                    throw new InvalidOrderItem($"The amount you ordered from product: {item.ProductId} is not in stock");
                }
            }
            catch (IdNotExist exc)
            {
                throw new InvalidOrderItem($"There is no product with ID number {item.ProductId}");
            }
        }
    }
    /// <summary>
    /// A private help function to check the correctness of the customer's email.
    /// </summary>
    private bool checkEmailValidation(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public BO.Cart UpdateAmountOfProd(BO.Cart cart, int id, int amount)
    {
        try
        {
            if (id < 1)
            {
                throw new InvalidValue("ID");
            }
            if (amount < 0)
            {
                throw new InvalidValue("amount");
            }
            bool exist = false;
            foreach (BO.OrderItem i in cart.Items)
            {
                if (i.ProductId == id)
                {
                    exist = true;//The product is in the shopping cart
                    if (i.Amount < amount)//Update in case the amount of the product increased
                    {
                        int inStock = Dal.product.Read(id).InStock;
                        if (inStock - amount < 0)
                        {
                            throw new OutOfStock(inStock);
                        }
                        cart.TotalPrice += i.Price * (amount - i.Amount);
                        i.Amount = amount;
                        i.TotalPrice = (i.Price) * amount;
                    }
                    else if (amount < i.Amount)//Update in case the amount of the product decreased
                    {
                        cart.TotalPrice -= i.Price * (i.Amount - amount);
                        if (amount == 0)
                        {
                            cart.Items.Remove(i);
                        }
                        else
                        {
                            i.Amount = amount;
                            i.TotalPrice = (i.Price) * amount;
                        }
                    }
                }
            }
            if (exist == false)//The product is not in the shopping cart
            {
                throw new ItemNotExist();
            }
            return cart;
        }
        catch (IdNotExist exc)
        {
            throw new DataError(exc);
        }
    }
}

