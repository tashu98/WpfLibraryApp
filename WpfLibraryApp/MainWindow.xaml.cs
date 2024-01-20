using System.Windows;
using WpfLibraryApp.DataAccess;
using WpfLibraryApp.Models;

namespace WpfLibraryApp;

public partial class MainWindow : Window
{
    private readonly Func<AddReaderWindow> _addReaderWindowFactory;
    private readonly Func<AddBookWindow> _addBookWindowFactory;
    private readonly Func<AddRentalWindow> _addRentalWindowFactory;
    private readonly Func<EditBookWindow> _editBookWindowFactory;
    private readonly Func<EditReaderWindow> _editReaderWindowFactory;

    private readonly AppDbContext _context;
    public Reader SelectedReader { get; set; }

    public MainWindow(MainWindowViewModel viewModel, AppDbContext context, Func<AddReaderWindow> addReaderWindowFactory, Func<AddBookWindow> addBookWindowFactory, Func<AddRentalWindow> addRentalWindowFactory, Func<EditBookWindow> editBookWindowFactory, Func<EditReaderWindow> editReaderWindowFactory)
    {
        _addReaderWindowFactory = addReaderWindowFactory;
        _addBookWindowFactory = addBookWindowFactory;
        _addRentalWindowFactory = addRentalWindowFactory;
        _editBookWindowFactory = editBookWindowFactory;
        _editReaderWindowFactory = editReaderWindowFactory;

        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));        _context = context;

        InitializeComponent();
    }


    void AddRentalButton_Click(object sender, RoutedEventArgs e)
    {
        var addRentalWindow = _addRentalWindowFactory();
        addRentalWindow.Owner = this;
        addRentalWindow.ShowDialog();
    }

    private void ReturnRentalButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedRental = dataGrid1.SelectedItem as Rental;
        if (selectedRental != null && selectedRental.ReturnDate == null)
        {
            selectedRental.ReturnDate = DateTime.Now;
            selectedRental.Book.Available++;
            selectedRental.Book.Reserved--;
            _context.SaveChanges();
        }
    }

    private void AddBookButton_Click(object sender, RoutedEventArgs e)
    {
        var addBookWindow = _addBookWindowFactory();
        addBookWindow.Owner = this;
        addBookWindow.ShowDialog();
    }

    private void EditBookButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedBook = dataGrid2.SelectedItem as Book;
        if (selectedBook != null)
        {
            var editBookWindow = _editBookWindowFactory();
            editBookWindow.LoadBook(selectedBook);
            editBookWindow.Owner = this;
            editBookWindow.ShowDialog();
        }
    }

    private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedBook = dataGrid2.SelectedItem as Book;
        if (selectedBook != null && selectedBook.Reserved < 1)
        {
            _context.Books.Remove(selectedBook);
            _context.SaveChanges();
        }
    }


    private void AddReaderButton_Click(object sender, RoutedEventArgs e)
    {
        var addReaderWindow = _addReaderWindowFactory();
        addReaderWindow.Owner = this;
        addReaderWindow.ShowDialog();
    }

    private void EditReaderButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedReader = dataGrid3.SelectedItem as Reader;
        if (selectedReader != null)
        {
            var editReaderWindow = _editReaderWindowFactory();
            editReaderWindow.LoadReader(selectedReader);
            editReaderWindow.Owner = this;
            editReaderWindow.ShowDialog();
        }
    }

    private void DeleteReaderButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedReader = dataGrid3.SelectedItem as Reader;
        var selectedReader = _context.Readers.FirstOrDefault(p => p.Id == SelectedReader.Id);
        if (selectedReader != null)
        {
            _context.Readers.Remove(SelectedReader);
            _context.SaveChanges();
        }
    }

}