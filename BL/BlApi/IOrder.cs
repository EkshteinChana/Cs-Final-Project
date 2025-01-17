﻿
using BO;
using System.Runtime.CompilerServices;

namespace BlApi;
/// <summary>
/// Interfaces for actions regarding to main logical entity-Order
/// </summary>
public interface IOrder
{
    /// <summary>
    /// A function to read the list of orders for manager screen
    /// </summary>
    /// <returns>An IEnumerable of OrderForList</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public IEnumerable<OrderForList?> ReadOrdsManager();
    /// <summary>
    /// A function to read the details of an order
    /// for manager screen and customer screen
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public Order ReadOrd(int orderId);
    /// <summary>
    /// A function to update the shipping date
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public Order UpdateOrdShipping(int orderId);
    /// <summary>
    /// A function to update the delivery date 
    /// (for Order Management Screen of a Manager )
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public Order UpdateOrdDelivery(int orderId);
    /// <summary>
    /// A function to update an order 
    /// (for Manager Screen)
    /// </summary>
    /// <returns>Order(a logical entity)</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public Order UpdateOrd(int orderId, int pId, int amount, BO.eUpdateOrder action);

    /// <summary>
    /// A function to get an OrderTracking entity for a specific order 
    /// </summary>
    /// <returns>TrackOrder(a logical entity)</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public BO.OrderTracking TrackOrder(int orderId);
    /// <summary>
    /// A function to get order whose latest status change is the oldest. 
    /// </summary>
    /// <returns>order Id</returns>
    [MethodImpl(MethodImplOptions.Synchronized)] 
    public int? GetOldestOrder();
}