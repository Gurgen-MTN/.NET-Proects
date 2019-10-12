using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static OhtaPark.MainWindow;

namespace OhtaPark
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.DisplayDateEnd = DateTime.Now;
        }
        public Client Client { get; private set; }

        public ClientWindow(Client c)
        {
            InitializeComponent();
            Client = c;
            this.DataContext = Client;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (firstnameTextBox.Text != "" && lastnameTextBox.Text != "" && datePicker.Text != "")
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Error.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
