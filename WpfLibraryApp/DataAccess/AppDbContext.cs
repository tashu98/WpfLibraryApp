using Microsoft.EntityFrameworkCore;
using WpfLibraryApp.Models;

namespace WpfLibraryApp.DataAccess;

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Rental> Rentals { get; set; }
}
