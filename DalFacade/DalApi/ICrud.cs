namespace DalApi;

public interface ICrud<T> where T : struct
{
    public  int Create(T t);
    public T ReadSingle(Func<T, bool> func);
    public T Read(int id);
    public IEnumerable<T> Read(Func<T,bool>? func=null);
    public void Update(T t);
    public void Delete(int id);
}




