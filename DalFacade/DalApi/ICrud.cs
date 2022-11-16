using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface ICrud<T> //where T : struct ???
    {
        
    }
}
/*int CreateOrder(Order order)
Order ReadOrder(int id)
Order[] ReadOrder()
 void UpdateOrder(Order order)
void DeleteOrder(int id)

/////////
CreateOrderItem(OrderItem orderItem)
OrderItem ReadOrderItem(int id)
OrderItem ReadOrderItem(int pId,int oId)
OrderItem[] ReadOrderItem()
OrderItem[] ReadOrderItemByOrderId(int oId)
void UpdateOrderItem(OrderItem orderItem)
DeleteOrderItem(int id)

////////
int CreateProduct(Product product)
 Product ReadProduct(int id)
Product[] ReadProduct()
void UpdateProduct(Product product)
void DeleteProduct(int id)*/
