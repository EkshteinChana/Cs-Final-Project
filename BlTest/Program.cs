using BlApi;
namespace BlTest;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        try
        {
            throw new InvalidValue("ID");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);      
        }
    }
}