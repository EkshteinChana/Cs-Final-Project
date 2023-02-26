using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

/// <summary>
/// Exception in Pl Layer In case the input type is not correct.
/// </summary>
public class InValidInputTypeException : Exception
{
    public readonly string msg;
    public InValidInputTypeException(string m) { msg = m; }
    public override string Message => $"The type of the {msg} is incorrect";
}
