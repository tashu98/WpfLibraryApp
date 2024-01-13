namespace WpfLibraryApp.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public required int Quantity { get; set; }
    public required int Available { get; set; }
    public required int Reserved { get; set; }
}
