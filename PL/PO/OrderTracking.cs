using System;
using System.Collections.ObjectModel;

namespace PL.PO;
/// <summary>
/// PO Order Tracking entity for the Order Tracking screen
/// </summary>
public class OrderTracking
{
    public int Id { get; set; }
    public BO.eOrderStatus? Status { get; set; }
    public ObservableCollection<Tuple<DateTime?, BO.eOrderStatus>>? OrderStatusByDate { get; set; }//List of pairs (date, description of order progress )
    public override string ToString() => $@"
        ID: {Id}
        status: {Status}
        ";
    public OrderTracking()
    {
        OrderStatusByDate = new();
    }
}


