using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Order Tracking entity for the Order Tracking screen
/// </summary>
public class OrderTracking
{
    public int Id { get; set; }
    public eOrderStatus status { get; set; }
    public Dictionary<DateTime, eOrderStatus> OrderStatusByDate { get; set; }//List of pairs (date, description of order progress )

    public override string ToString() => $@"
        ID: {Id}
        status: {status}
        ";
}
