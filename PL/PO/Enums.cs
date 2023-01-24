using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO;
public enum eCategory
{
    Siddur,
    Tehillim,
    Shabbat,
    Chaggim,
    Others
}

public enum eOrderStatus
{
    confirmed,
    Sent,
    provided
}


public enum eUpdateOrder
{
    add,
    delete,
    changeAmount,
    noChanges
}

