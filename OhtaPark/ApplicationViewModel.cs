using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OhtaPark.MainWindow;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;


namespace OhtaPark
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand removeCommand;
        IEnumerable<Client> clients;

        public IEnumerable<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public ApplicationViewModel()
        {
            db = new ApplicationContext();
            db.Clients.Load();
            Clients = db.Clients.Local.ToBindingList();
        }
        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      ClientWindow clientWindow = new ClientWindow(new Client());
                      if (clientWindow.ShowDialog() == true)
                      {
                          Client client = clientWindow.Client;
                          client.Birthday = clientWindow.Client.Birthday.Substring(0, clientWindow.Client.Birthday.IndexOf(" "));
                          db.Clients.Add(client);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Client client = selectedItem as Client;

                      Client vm = new Client()
                      {
                          Id = client.Id,
                          FirstName = client.FirstName,
                          LastName = client.LastName,
                          Birthday = client.Birthday
                      };
                      ClientWindow clientWindow = new ClientWindow(vm);


                      if (clientWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          client = db.Clients.Find(clientWindow.Client.Id);
                          if (client != null)
                          {
                              client.FirstName = clientWindow.Client.FirstName;
                              client.LastName = clientWindow.Client.LastName;
                              if (clientWindow.Client.Birthday.IndexOf(" ") !=-1)
                              {
                                  client.Birthday = clientWindow.Client.Birthday.Substring(0, clientWindow.Client.Birthday.IndexOf(" "));
                              }
                              
                              db.Entry(client).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      Client client = selectedItem as Client;
                      db.Clients.Remove(client);
                      db.SaveChanges();
                  }));
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
