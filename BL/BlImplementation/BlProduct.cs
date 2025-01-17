﻿using DalApi;
using BlApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = DalApi.Factory.Get() ?? throw new Exception("Can not get BlImplementation.BL");

    /// <summary>
    /// A private help function for checking the integrity of the data in the logical layer for adding/updating a product.
    /// </summary>
    private void checkProdValues(BO.Product prod)
    {
        if (prod.Id < 1)
        {
            throw new InvalidValueException("ID");
        }
        if (string.IsNullOrEmpty(prod.Name))
        {
            throw new InvalidValueException("name");
        }
        if (!(prod.Price is double val))
        {
            throw new InvalidValueException("price");
        }
        if (prod.Price <= 0)
        {
            throw new InvalidValueException("price");
        }
        if (((int?)prod.Category < 0) || ((int?)prod.Category > 4))
        {
            throw new InvalidValueException("category");
        }
        if (!(prod.InStock is int))
        {
            throw new InvalidValueException("amount in stock");
        }
        if (prod.InStock < 0)
        {
            throw new InvalidValueException("amount in stock");
        }
    }

    /// <summary>
    /// A private help function to convert DO.Product entity to BO.ProductForList entity.
    /// </summary>
    private BO.ProductForList convertDoProdToBoProdForLst(DO.Product dP)
    {
        BO.ProductForList bP = new BO.ProductForList();
        bP.GetType().GetProperties().Where(bPr => bPr.Name != "InStock" && bPr.Name != "Category").Select(bPr => { bPr.SetValue(bP, dP.GetType().GetProperty(bPr.Name)?.GetValue(dP)); return bPr; }).ToList();
        bP.Category = (BO.eCategory?)dP.Category;
        return bP;
    }

    /// <summary>
    /// A private help function to convert BO.Product entity to DO.Product entity.
    /// </summary>
    private DO.Product convertBoProdToDoProd(BO.Product prod)
    {
        object tmpNewProd = new DO.Product();
        tmpNewProd.GetType().GetProperties().Where(prop => prop.Name != "Category").Select(prop =>
        {
            prop.SetValue(tmpNewProd, prod.GetType().GetProperty(prop.Name)?.GetValue(prod));
            return prop;
        }).ToList();
        DO.Product newProd = (DO.Product)tmpNewProd;
        newProd.Category = (DO.eCategory?)prod.Category;
        return newProd;
    }

    /// <summary>
    /// A function that receives product data, checks its integrity 
    /// and sends a request to the data layer to add such a product.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int CreateProd(BO.Product prod)
    {
        int id = 0;
        prod.Id = 111;//Temporary value for the checkOrdValues function - will be updated later.
        checkProdValues(prod);
        DO.Product newProd = convertBoProdToDoProd(prod);
        bool tryId = true;
        while (tryId)//Create an ID
        {
            tryId = false;
            try
            {
                Random rnd = new Random();
                newProd.Id = rnd.Next(100000, 1000000);
                lock (Dal)
                {
                    id = Dal.product.Create(newProd);
                }
            }
            catch (IdAlreadyExistsException)
            {
                tryId = true;
            }
        }
        return id;
    }

    /// <summary>
    /// A function that receives a product ID, checks that it does not exist in orders 
    /// and sends a request to the data layer to delete it from the list
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteProd(int Id)
    {
        lock (Dal)
        {
            IEnumerable<DO.OrderItem> orderItemsList = Dal.orderItem.Read();

            DO.OrderItem ordWithProd = orderItemsList.Where(o => o.ProductId == Id).FirstOrDefault();
            if (!ordWithProd.Equals(default(DO.OrderItem)))
            {
                throw new IllegalActionException("It is not possible to delete an existing product in an order");
            }
            try
            {
                Dal.product.Delete(Id);
            }

            catch (IdNotExistException err)
            {
                throw new DataErrorException(err, "Data Error: ");
            }
        }
    }
    /// <summary>
    /// A function that returns an entity to display product data for a customer screen 
    /// by referring to the data layer using an ID.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem ReadProdCustomer(int Id, BO.Cart cart)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidValueException("ID");
            }
            lock (Dal)
            {
                DO.Product dP = Dal.product.Read(Id);

                BO.ProductItem bP = new();
                bP.GetType().GetProperties().Where(bPr => bPr.Name != "InStock" && bPr.Name != "Category").Select(bPr => { bPr.SetValue(bP, dP.GetType().GetProperty(bPr.Name)?.GetValue(dP)); return bPr; }).ToList();
                bP.Category = (BO.eCategory?)dP.Category;
                bP.InStock = (dP.InStock > 0) ? true : false;

                bool exist = false;
                if (cart != null && cart.Items != null)
                {
                    cart.Items.Where(i => i?.ProductId == bP.Id).Select(i => { exist = true; bP.Amount = i.Amount; return i; }).ToList();
                }
                if (exist == false)
                {
                    bP.Amount = 0;
                }
                return bP;
            }
        }
        catch (IdNotExistException exc)
        {
            throw new DataErrorException(exc, "Data Error: ");
        }
    }

    /// <summary>
    /// A function that returns an entity to display product data for the manager screen 
    /// by referring to the data layer using an ID.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product ReadProdManager(int Id)
    {
        try
        {
            if (Id < 1)
            {
                throw new InvalidValueException("ID");
            }
            lock (Dal)
            {
                DO.Product dP = Dal.product.Read(Id);
                BO.Product bP = new BO.Product();
                bP.GetType().GetProperties().Where(prop => prop.Name != "Category").Select(prop => { prop.SetValue(bP, dP.GetType().GetProperty(prop.Name)?.GetValue(dP)); return prop; }).ToList();
                bP.Category = (BO.eCategory?)dP.Category;
                return bP;
            }
        }
        catch (IdNotExistException exc)
        {
            throw new DataErrorException(exc, "Data Error: ");
        }
    }

    /// <summary>
    /// A function to read the list of products
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> ReadProdsList(BO.eCategory? category = null)
    {
        List<BO.ProductForList> bProdsList = new();
        List<DO.Product> dProds = new();
        if (category == null)
        {
            lock (Dal)
            {
                dProds = Dal.product.Read().ToList();
            }
        }
        else
        {
            DO.eCategory ctgry = (DO.eCategory)category;
            lock (Dal)
            {
                dProds = Dal.product.Read((DO.Product p) => p.Category == ctgry).ToList();
            }
        }
        dProds.OrderBy(dP => dP.Name).Select(dP =>
        {
            BO.ProductForList bP = convertDoProdToBoProdForLst(dP);
            bProdsList.Add(bP);
            return dProds;
        }).ToList();
        return bProdsList;
    }

    /// <summary>
    /// A function that receives product data, checks their integrity
    ///and sends a request to the data layer to update the product with such an ID.
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateProd(BO.Product prod)
    {
        checkProdValues(prod);
        DO.Product newProd = convertBoProdToDoProd(prod);
        try
        {
            lock (Dal)
            {
                Dal.product.Update(newProd);
            }
        }
        catch (IdNotExistException err)
        {
            throw new DataErrorException(err, "Data Error: ");
        }
    }

}
