using Supermarket.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Supermarket.Model;
using Supermarket.Model.BusinessLogicLayer;

namespace Supermarket.ViewModel
{
    internal class UserVM : BasePropertyChanged
    {
        private UserBLL persBLL;

        public UserVM()
        {
            persBLL = new UserBLL();
            PersonsList = persBLL.UsersList;
        }

        public ObservableCollection<User> PersonsList
        {
            get => persBLL.UsersList;
            set
            {
                persBLL.UsersList = value;
                NotifyPropertyChanged("PersonsList");
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        public void AddMethod(object obj)
        {
            persBLL.AddMethod(obj);
            ErrorMessage = persBLL.ErrorMessage;
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddMethod);
                }
                return addCommand;
            }
        }

        public void UpdateMethod(object obj)
        {
            persBLL.UpdateMethod(obj);
            ErrorMessage = persBLL.ErrorMessage;
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(UpdateMethod);
                }
                return updateCommand;
            }
        }

        public void DeleteMethod(object obj)
        {
            persBLL.DeleteMethod(obj);
            ErrorMessage = persBLL.ErrorMessage;
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(DeleteMethod);
                }
                return deleteCommand;
            }
        }
    }

}
