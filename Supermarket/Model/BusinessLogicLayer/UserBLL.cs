using Supermarket.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Supermarket.Model;
namespace Supermarket.Model.BusinessLogicLayer
{
    public class UserBLL
    {
        private SupermarketContext context = new SupermarketContext();
        public ObservableCollection<User> UsersList { get; set; }
        public string ErrorMessage { get; set; }

        public UserBLL()
        {
            UsersList = new ObservableCollection<User>(context.Utilizatori.ToList());
        }

        public void AddMethod(object obj)
        {
            User pers = obj as User;
            if (pers != null)
            {
                if (string.IsNullOrEmpty(pers.NumeUtilizator)||string.IsNullOrEmpty(pers.Parola)|| string.IsNullOrEmpty(pers.TipUtilizator))
                {
                    ErrorMessage = "Campurile trebuie precizate";
                }
                else
                {
                    context.Utilizatori.Add(pers);
                    context.SaveChanges();
                    UsersList.Add(pers);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            User pers = obj as User;
            if (pers == null)
            {
                ErrorMessage = "Selecteaza o persoana";
            }
            else if (string.IsNullOrEmpty(pers.NumeUtilizator) || string.IsNullOrEmpty(pers.Parola) || string.IsNullOrEmpty(pers.TipUtilizator))
            {
                ErrorMessage = "Campurile trebuie precizate";
            }
            else
            {
                User p = context.Utilizatori.Find(pers.Id);
                if (p != null)
                {
                    p.NumeUtilizator = pers.NumeUtilizator;
                    p.Parola = pers.Parola;
                    p.TipUtilizator = pers.TipUtilizator;
                    context.SaveChanges();
                    ErrorMessage = "";
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            User pers = obj as User;
            if (pers == null)
            {
                ErrorMessage = "Selecteaza o persoana";
            }
            else
            {
                User p = context.Utilizatori.Find(pers.Id);
                if (p != null)
                {
                    context.Utilizatori.Remove(p);
                    context.SaveChanges();
                    UsersList.Remove(pers);
                    ErrorMessage = "";
                }
            }
        }
    }

}
