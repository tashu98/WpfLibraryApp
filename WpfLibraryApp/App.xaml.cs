using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Threading;
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
            .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {

            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<WeakReferenceMessenger>();
                services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

                services.AddSingleton<Dispatcher>(_ => Current.Dispatcher);

                services.AddTransient<AddReaderWindow>();
                services.AddTransient<AddBookWindow>();
                services.AddTransient<AddRentalWindow>();
                services.AddTransient<EditBookWindow>();
                services.AddTransient<EditReaderWindow>();
                services.AddTransient<Func<AddReaderWindow>>(serviceProvider => () => serviceProvider.GetRequiredService<AddReaderWindow>());
                services.AddTransient<Func<AddBookWindow>>(serviceProvider => () => serviceProvider.GetRequiredService<AddBookWindow>());
                services.AddTransient<Func<AddRentalWindow>>(serviceProvider => () => serviceProvider.GetRequiredService<AddRentalWindow>());
                services.AddTransient<Func<EditBookWindow>>(serviceProvider => () => serviceProvider.GetRequiredService<EditBookWindow>());
                services.AddTransient<Func<EditReaderWindow>>(serviceProvider => () => serviceProvider.GetRequiredService<EditReaderWindow>());

                services.AddDbContext<AppDbContext>(
                    options =>
                    {
                        options.UseSqlite("Data Source=.\\library.db");
                        //options.UseLazyLoadingProxies();
                    });
            });

    private static async Task PopulateDatabase(AppDbContext context)
    {
        if (await context.Books.AnyAsync())
        {
            return;
        }

        var authors = new List<string> { "J.K. Rowling", "Stephen King", "George R.R. Martin", "J.R.R. Tolkien", "Agatha Christie" };
        var titles = new List<string> { "Harry Potter", "The Shining", "Game of Thrones", "The Lord of the Rings", "Murder on the Orient Express" };
        var descriptions = new List<string> { "A series about a young wizard", "A horror novel set in an isolated hotel", "A fantasy series about a battle for a throne", "A fantasy epic about a quest to destroy a powerful ring", "A detective novel about a murder on a train" };
        var isbns = new List<string> { "978-0747532699", "978-0307743657", "978-0553103540", "978-0261102385", "978-0007119318" };

        for (int i = 0; i < 5; i++)
        {
            var quantity = new Random().Next(1, 100);
            var reserved = new Random().Next(0, quantity);
            var available = quantity - reserved;

            context.Books.Add(new Book
            {
                Title = titles[i],
                Author = authors[i],
                Description = descriptions[i],
                ISBN = isbns[i],
                Quantity = quantity,
                Available = available,
                Reserved = reserved
            });
        }

        await context.SaveChangesAsync();

        var names = new List<string> { "John Doe", "Jane Smith", "Bob Johnson", "Alice Williams", "Charlie Brown" };
        var addresses = new List<string> { "123 Main St", "456 Oak St", "789 Pine St", "321 Elm St", "654 Maple St" };
        var emails = new List<string> { "john@example.com", "jane@example.com", "bob@example.com", "alice@example.com", "charlie@example.com" };
        var phones = new List<string> { "555-1234", "555-5678", "555-9012", "555-3456", "555-7890" };

        for (int i = 0; i < 5; i++)
        {
            context.Readers.Add(new Reader
            {
                Name = names[i],
                Address = addresses[i],
                Email = emails[i],
                Phone = phones[i],
                DateOfBirth = DateTime.Now.AddYears(-new Random().Next(20, 70))
            });
        }

        await context.SaveChangesAsync();

        for (int i = 0; i < 5; i++)
        {
            //get random book from database
            Book book = await context.Books.Skip(i).FirstOrDefaultAsync();
            //get random reader from database
            Reader reader = await context.Readers.Skip(i).FirstOrDefaultAsync();

            if (book != null && reader != null)
            {
                context.Rentals.Add(new Rental
                {
                    RentalDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(7),
                    Book = book,
                    Reader = reader
                });
            }
        }

        await context.SaveChangesAsync();
    }
}


