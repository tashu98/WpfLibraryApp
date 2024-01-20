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
    /// Interaction logic for EditReaderWindow.xaml
    /// </summary>
    public partial class EditReaderWindow : Window
    {
        private readonly AppDbContext _context;
        private Reader _readerToEdit;

        public EditReaderWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public void LoadReader(Reader readerToEdit)
        {
            _readerToEdit = readerToEdit;

            // Load the current reader details into the TextBoxes
            txtName.Text = _readerToEdit.Name;
            txtAddress.Text = _readerToEdit.Address;
            txtEmail.Text = _readerToEdit.Email;
            txtPhone.Text = _readerToEdit.Phone;
            dpDateOfBirth.SelectedDate = _readerToEdit.DateOfBirth;
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the reader with the provided information
            _readerToEdit.Name = txtName.Text;
            _readerToEdit.Address = txtAddress.Text;
            _readerToEdit.Email = txtEmail.Text;
            _readerToEdit.Phone = txtPhone.Text;
            _readerToEdit.DateOfBirth = dpDateOfBirth.SelectedDate;
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
