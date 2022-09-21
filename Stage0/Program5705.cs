// See https://aka.ms/new-console-template for more information
//using System;
//namespace Stage0;
partial class Program
{
    static void Main()
    {
        welcome5705();
        welcome8631();
        Console.ReadKey();
    }
    private static void welcome5705()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }
    static partial void welcome8631();
}
