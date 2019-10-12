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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OhtaPark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        ApplicationContext db=new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
            //db = new ApplicationContext();
            //db.Clients.Load();
            //this.DataContext = db.Clients.Local.ToBindingList();

            this.DataContext = new ApplicationViewModel();
        }

        public class Client : INotifyPropertyChanged
        {
            private string firstName;
            private string lastName;
            private string birthday;

            public int Id { get; set; }

            public string FirstName
            {
                get { return firstName; }
                set
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
            public string LastName
            {
                get { return lastName; }
                set
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
            public string Birthday
            {
                get { return birthday; }
                set
                {
                    birthday = value;
                    OnPropertyChanged("Birthday");
                }
            }



            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

       
    }
}
