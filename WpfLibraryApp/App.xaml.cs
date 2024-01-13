using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfLibraryApp.DataAccess;
using WpfLibraryApp.Models;

namespace WpfLibraryApp;

public partial class App : Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        host.Start();


        App app = new App();
        app.InitializeComponent();

        using (var scope = host.Services.CreateScope())
        using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
        {
            dbContext.Database.Migrate();

            Task.Run(async () => await PopulateDatabase(dbContext)).Wait();
        }


        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;

        app.Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainWindow>();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite("Data Source=.\\library.db");
                });
            });
    private static async Task PopulateDatabase(AppDbContext context)
    {
        if (await context.Books.AnyAsync())
        {
            return;
        }
        for (int i = 0; i < 10; i++)
        {
            context.Books.Add(new Book
            {
                Title = $"Book {i}",
                Author = $"Author {i}",
                Description = $"Description {i}",
                ISBN = $"ISBN {i}",
                Quantity = i,
                Available = i,
                Reserved = i
            });
            await context.SaveChangesAsync();
        }
        for (int i = 0; i < 10; i++)
        {
            context.Readers.Add(new Reader
            {
                Name = $"Reader {i}",
                Address = $"Address {i}",
                Email = $"Email {i}",
                Phone = $"Phone {i}",
                DateOfBirth = DateTime.Now.AddYears(-i)
            });
            await context.SaveChangesAsync();
        }
        for (int i = 0; i < 10; i++)
        {
            //get random book from database
            Book book = await context.Books.Skip(i)
                .FirstOrDefaultAsync();
            //get random reader from database
            Reader reader = await context.Readers.Skip(i)
                .FirstOrDefaultAsync();

            context.Rentals.Add(new Rental
            {
                RentalDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                Book = book,
                Reader = reader
            });
            await context.SaveChangesAsync();
        }
    }
}


