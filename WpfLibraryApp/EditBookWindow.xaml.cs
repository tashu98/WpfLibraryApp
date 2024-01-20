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
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {
        private readonly AppDbContext _context;
        private Book _bookToEdit;

        public EditBookWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public void LoadBook(Book bookToEdit)
        {
            _bookToEdit = bookToEdit;

            // Load the current book details into the TextBoxes
            txtTitle.Text = _bookToEdit.Title;
            txtAuthor.Text = _bookToEdit.Author;
            txtDescription.Text = _bookToEdit.Description;
            txtISBN.Text = _bookToEdit.ISBN;
            txtQuantity.Text = _bookToEdit.Quantity.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the book with the provided information
            _bookToEdit.Title = txtTitle.Text;
            _bookToEdit.Author = txtAuthor.Text;
            _bookToEdit.Description = txtDescription.Text;
            _bookToEdit.ISBN = txtISBN.Text;
            _bookToEdit.Quantity = int.Parse(txtQuantity.Text);
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
