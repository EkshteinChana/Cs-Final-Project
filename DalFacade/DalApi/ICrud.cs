namespace DalApi;

public interface ICrud<T> where T : struct
{
    public  int Create(T t);
    T Read(int id);
    IEnumerable<T?> Read(Func<T?,bool> func=null);
    void Update(T t);
    void Delete(int id);
}




