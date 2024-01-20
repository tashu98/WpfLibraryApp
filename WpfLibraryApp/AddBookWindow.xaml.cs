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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private readonly AppDbContext _context;

        public AddBookWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var newBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Description = txtDescription.Text,
                ISBN = txtISBN.Text,
                Quantity = int.Parse(txtQuantity.Text),
                Available = int.Parse(txtQuantity.Text), // Assuming all added books are available
                Reserved = 0 // Assuming no books are reserved when added
            };
            _context.Books.Add(newBook);
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
