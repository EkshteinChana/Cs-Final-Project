using BlApi;
using DalApi;
using Dal;
using System.Xml.Linq;

namespace BlImplementation;
internal class BlCart : ICart
{
    private IDal dalList = DalApi.Factory.Get();
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
            DO.Product dP = dalList.product.Read(id);

            if (cart.Items.Count !=0)
                cart.Items.Where(i => i?.ProductId == id) //The product is already in the shopping cart
                     .Select(i =>
                     {
                         exist = true;
                         if (dP.InStock <= 0)
                         {
                             throw new OutOfStock(dP.Id, 0);
                         }
                         i.Amount += 1;
                         i.TotalPrice += i.Price;
                         cart.TotalPrice += i.Price;
                         return i;
                     }).ToList();
            if (exist == true)
            {
                return cart;
            }
            //The product is not in the shopping cart
            if (dP.InStock <= 0)
            {
                throw new OutOfStock(dP.Id, 0);
            }
            BO.OrderItem oI = new BO.OrderItem()
            {
                Id = Config.MaxCartOrderItemId,
                ProductId = dP.Id,
                Price = dP.Price,
                Name = dP.Name,
                Amount = 1,
                TotalPrice = dP.Price
            };
            if (cart.Items == null)
            {
                cart.Items = new List<BO.OrderItem?>();
            }
            cart.Items.Add(oI);
            cart.TotalPrice += dP.Price;
            return cart;
        }
        catch (IdNotExistException exc)
        {
            throw new DataError(exc, "Data Error: ");
        }
    }

    public void MakeOrder(BO.Cart cart, string customerName, string customerEmail, string customerAddress)
    {
        checkDataMakeOrder(cart, customerName, customerEmail, customerAddress);
        DO.Order dOrder = new DO.Order()
        {
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            CustomerAddress = customerAddress,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null
        };
        bool tryId = true;
        int orderId = 0;
        while (tryId)
        {
            tryId = false;
            try
            {
                //for xml
                //XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\config.xml").Root;
                //dOrder.Id = Convert.ToInt32(root.Element("MaxOrderId").Value.ToString());
                //root.Element("MaxOrderId").Value = Convert.ToString(dOrder.Id + 1);
                //root?.Save("..\\..\\..\\..\\xml\\config.xml");
                //for list
                dOrder.Id = DataSource.Config.MaxOrderId;
                orderId = dalList.order.Create(dOrder);
            }
            catch (IdAlreadyExistsException)
            {
                tryId = true;
            }
        }
        if (cart.Items != null)
        {
            cart.Items.Select(item =>
            { //Building order item objects in the data layer based on items ordered in the shopping cart
                object tmpdOrderItem = new DO.OrderItem();
                tmpdOrderItem.GetType().GetProperties().Where(prop => prop.Name != "OrderId").Select(prop =>
                    {
                        prop.SetValue(tmpdOrderItem, item?.GetType()?.GetProperty(prop.Name)?.GetValue(item));
                        return prop;
                    }).ToList();
                DO.OrderItem dOrderItem = (DO.OrderItem)tmpdOrderItem;
                dOrderItem.OrderId = orderId;
                tryId = true;
                while (tryId)
                {
                    tryId = false;
                    try
                    {
                        ///////////////////////////////////////////
                        //for xml
                        ///////////////////////////////////////////
                        //XElement? root = XDocument.Load("..\\..\\..\\..\\xml\\config.xml").Root;
                        //dOrderItem.Id = Convert.ToInt32(root.Element("MaxOrderItemId").Value.ToString());
                        //root.Element("MaxOrderItemId").Value = Convert.ToString(dOrderItem.Id + 1);
                        //root?.Save("..\\..\\..\\..\\xml\\config.xml");
                        //for list
                        dOrderItem.Id = DataSource.Config.MaxOrderItemId;
                        dalList.orderItem.Create(dOrderItem);
                    }
                    catch (IdAlreadyExistsException)
                    {
                        tryId = true;
                    }
                }
                return item;
            }).ToList();

            //Updating the amount in stock in the data layer of the ordered products
            cart.Items.Select(item =>
            {
                try
                {
                    DO.Product dP = dalList.product.Read(item.ProductId);
                    if (dP.InStock < item.Amount)
                    {
                        throw new OutOfStock(dP.Id, dP.InStock);
                    }
                    dP.InStock -= item.Amount;
                    dalList.product.Update(dP);
                }
                catch (IdNotExistException exc)
                {
                    throw new DataError(exc, $"invalid ID of product ID: {item.ProductId} ,Data Error: ");
                }
                return item;
            }).ToList();
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

        if (cart.Items != null)
        {
            //foreach (BO.OrderItem item in cart.Items)
            //{
            //    if (item.Amount < 1)
            //    {
            //        throw new InvalidValue($"amount of product: {item.ProductId}");
            //    }
            //    try
            //    {
            //        DO.Product dP = dalList.product.Read(item.ProductId);
            //        if (dP.InStock < item.Amount)
            //        {
            //            throw new OutOfStock(dP.Id, dP.InStock);
            //        }
            //    }
            //    catch (IdNotExistException exc)
            //    {
            //        throw new InvalidValue($"ID of product ID: {item.ProductId}");
            //    }
            //}
            cart.Items.Select(item =>
            {
                if (item?.Amount < 1)
                {
                    throw new InvalidValue($"amount of product: {item.ProductId}");
                }
                try
                {
                    DO.Product dP = dalList.product.Read(item.ProductId);
                    if (dP.InStock < item.Amount)
                    {
                        throw new OutOfStock(dP.Id, dP.InStock);
                    }
                }
                catch (IdNotExistException exc)
                {
                    throw new InvalidValue($"ID of product ID: {item.ProductId}");
                }
                return item;
            }).ToList();
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
            cart.Items.Select(i =>
            {
                if (i?.ProductId == id)
                {
                    exist = true;//The product is in the shopping cart
                    if (i.Amount < amount)//Update in case the amount of the product increased
                    {
                        DO.Product dP = dalList.product.Read(id);
                        if (dP.InStock - amount < 0)
                        {
                            throw new OutOfStock(dP.Id, dP.InStock);
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
                return i;
            }).ToList();

            if (exist == false)//The product is not in the shopping cart
            {
                throw new ItemNotExist();
            }
            return cart;
        }
        catch (IdNotExistException exc)
        {
            throw new DataError(exc, "Data Error: ");
        }
    }
}

