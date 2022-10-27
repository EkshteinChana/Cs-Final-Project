
namespace DO;
    public struct Product
    {
    private readonly int Id { get; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int InStock { get; set; }
    public eCategory Category { get; set; }
}
