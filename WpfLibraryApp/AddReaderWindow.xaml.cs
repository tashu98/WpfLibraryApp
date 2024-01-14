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

using System.Windows;
using System.Xml.Linq;
using WpfLibraryApp.Models;

namespace WpfLibraryApp
{
    public partial class AddReaderWindow : Window
    {
        public Reader CreatedReader { get; private set; }

        public AddReaderWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Reader with the provided information
            CreatedReader = new Reader
            {
                // Set properties based on the user input
                Id = 1,
                Name = txtName.Text
            };

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

