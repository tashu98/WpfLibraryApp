using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfLibraryApp.DataAccess;
using WpfLibraryApp.Models;

namespace WpfLibraryApp
{
    /// <summary>
    /// Interaction logic for AddRentalWindow.xaml
    /// </summary>
    public partial class AddRentalWindow : Window
    {
        private readonly AppDbContext _context;

        public AddRentalWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;

            // Load readers and books into the ComboBoxes
            cmbReaders.ItemsSource = _context.Readers.ToList();
            cmbReaders.DisplayMemberPath = "Name";
            cmbReaders.SelectedValuePath = "Id";

            cmbBooks.ItemsSource = _context.Books.Where(b => b.Available > 0).ToList();
            cmbBooks.DisplayMemberPath = "Title";
            cmbBooks.SelectedValuePath = "Id";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Rental with the selected Reader and Book
            var newRental = new Rental
            {
                RentalDate = DateTime.Now,
                Reader = _context.Readers.Find(cmbReaders.SelectedValue),
                Book = _context.Books.Find(cmbBooks.SelectedValue)
            };
            _context.Rentals.Add(newRental);

            // Update the available quantity of the selected book
            newRental.Book.Available--;

            _context.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
