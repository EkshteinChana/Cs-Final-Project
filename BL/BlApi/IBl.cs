using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBl
    {
        public IProduct Product { get; }
        public IOrder Ordrt { get; }
        public ICart Cart { get; }
    }
}
