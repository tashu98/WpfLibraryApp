namespace WpfLibraryApp.Models;

public class Rental
{
    public int Id { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Book? Book { get; set; }
    public Reader? Reader { get; set; }
}
