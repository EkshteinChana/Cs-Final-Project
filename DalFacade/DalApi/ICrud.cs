namespace DalApi
{
    public interface ICrud<T> //where T : struct ???
    {
        int Create(T t);
        T Read(int id);
        IEnumerable<T> Read();
        void Update(T t);
        void Delete(int id);
    }
}






///////// to add 
//OrderItem ReadOrderItem(int pId,int oId)
//OrderItem[] ReadOrderItemByOrderId(int oId)

