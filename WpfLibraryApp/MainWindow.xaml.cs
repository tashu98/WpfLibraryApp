
using System;
using System.Windows;
using WpfLibraryApp.Models;

namespace WpfLibraryApp;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
    }

    private void AddReaderButton_Click(object sender, RoutedEventArgs e)
    {
        // Create and show the AddReaderWindow
        AddReaderWindow addReaderWindow = new AddReaderWindow();
        addReaderWindow.Owner = this; // Set the main window as the owner
        addReaderWindow.ShowDialog();

        // After the AddReaderWindow is closed, you can access the created reader if needed
        //if (addReaderWindow.DialogResult.HasValue && addReaderWindow.DialogResult.Value)
        //{
        //    Reader newReader = addReaderWindow.CreatedReader;
        //    // Do something with the new reader, e.g., add it to the Readers collection
        //    Readers.Add(newReader);
        //}
    }
}