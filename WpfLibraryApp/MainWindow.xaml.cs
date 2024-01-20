
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfLibraryApp.DataAccess;
using WpfLibraryApp.Models;

namespace WpfLibraryApp;

public partial class MainWindow : Window
{

    private readonly AddReaderWindow _addReaderWindow;
    private readonly AddBookWindow _addBookWindow;
    private readonly AddRentalWindow _addRentalWindow;
    private readonly EditBookWindow _editBookWindow;
    private readonly EditReaderWindow _editReaderWindow;

    private readonly AppDbContext _context;
    public Reader SelectedReader { get; set; }

    public MainWindow(MainWindowViewModel viewModel, AppDbContext context, AddReaderWindow addReaderWindow, AddBookWindow addBookWindow, AddRentalWindow addRentalWindow, EditBookWindow editBookWindow, EditReaderWindow editReaderWindow)
    {
        _addReaderWindow = addReaderWindow;
        _addBookWindow = addBookWindow;
        _addRentalWindow = addRentalWindow;
        _editBookWindow = editBookWindow;
        _editReaderWindow = editReaderWindow;

        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));        _context = context;

        InitializeComponent();
    }

       
    void AddRentalButton_Click(object sender, RoutedEventArgs e)
    {
        _addRentalWindow.Owner = this;
        _addRentalWindow.ShowDialog();
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
        _addBookWindow.Owner = this;
        _addBookWindow.ShowDialog();
    }

    private void EditBookButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedBook = dataGrid2.SelectedItem as Book;
        if (selectedBook != null)
        {
            _editBookWindow.LoadBook(selectedBook);
            _editBookWindow.Owner = this;
            _editBookWindow.ShowDialog();
        }
    }

    private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedBook = dataGrid2.SelectedItem as Book;
        if (selectedBook != null)
        {
            _context.Books.Remove(selectedBook);
            _context.SaveChanges();
        }
    }


    private void AddReaderButton_Click(object sender, RoutedEventArgs e)
    {
        _addReaderWindow.Owner = this;
        _addReaderWindow.ShowDialog();

    }

    private void EditReaderButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedReader = dataGrid3.SelectedItem as Reader;
        if (selectedReader != null)
        {
            _editReaderWindow.LoadReader(selectedReader);
            _editReaderWindow.Owner = this;
            _editReaderWindow.ShowDialog();
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