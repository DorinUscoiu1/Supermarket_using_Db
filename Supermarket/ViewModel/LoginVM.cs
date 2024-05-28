using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Supermarket.Model;
using Supermarket.View;

namespace Supermarket.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(Login);
            User = new User();
        }

        public void Login(object parameter)
        {

            using (var context = new SupermarketContext())
            {
                User userIntrodus=parameter as User;
                var user = context.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == userIntrodus.NumeUtilizator && u.Parola==userIntrodus.Parola);
                if (user != null)
                {
                    if (user.TipUtilizator == "admin")
                    {

                        var adminView = new AdminView();
                        adminView.DataContext = new AdminVM();
                        adminView.Show();
                    }
                    else
                    {
                        var casierView = new CasierView();
                        var casierVM = new CasierVM(); 
                        casierView.DataContext = casierVM;
                        casierView.Show();
                    }
                }
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
