﻿using System;
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
using WpfLibraryApp.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace WpfLibraryApp
{
    public partial class AddReaderWindow : Window
    {
        private readonly AppDbContext _context;
        public Reader CreatedReader { get; private set; }

        public AddReaderWindow(AppDbContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Reader with the provided information
            CreatedReader = new Reader
            {
                // Set properties based on the user input
                Name = txtName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                DateOfBirth = dataPicker.SelectedDate
            };
            
            _context.Readers.Add(CreatedReader);
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

