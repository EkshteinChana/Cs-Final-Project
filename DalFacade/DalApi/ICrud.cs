namespace DalApi;

public interface ICrud<T> where T : struct
{
    public  int Create(T t);
    T Read(Func<T, bool> func);
  IEnumerable<T> Read(Func<T,bool>? func=null);
    void Update(T t);
    void Delete(int id);
}




