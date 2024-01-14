

using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using WpfLibraryApp.DataAccess;
using WpfLibraryApp.Models;

namespace WpfLibraryApp;

public class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<Rental> Rentals { get; set; }
    public ObservableCollection<Book> Books { get; set; }
    public ObservableCollection<Reader> Readers { get; set; }

    public MainWindowViewModel(AppDbContext context)
    {
        context.Rentals.Load();
        Rentals = context.Rentals.Local.ToObservableCollection();

        context.Books.Load();
        Books = context.Books.Local.ToObservableCollection();

        context.Readers.Load();
        Readers = context.Readers.Local.ToObservableCollection();
    }

}