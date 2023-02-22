using System.Runtime.CompilerServices;

namespace BlApi;

public class Factory
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static IBl? Get()
    {
        return new BlImplementation.Bl();
    }      
}

