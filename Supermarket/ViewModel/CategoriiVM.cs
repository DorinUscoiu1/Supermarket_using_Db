using Supermarket.Helper;
using Supermarket.Model.BusinessLogicLayer;
using Supermarket.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Supermarket.ViewModel
{
    public class CategoriiVM : BasePropertyChanged
    {
        private CategorieBLL categorieBLL;

        public CategoriiVM()
        {
            categorieBLL = new CategorieBLL();
            CategoriiList = categorieBLL.CategoriiList;
        }

        public ObservableCollection<Categorie> CategoriiList
        {
            get => categorieBLL.CategoriiList;
            set
            {
                categorieBLL.CategoriiList = value;
                NotifyPropertyChanged("CategoriiList");
            }
        }

        private Categorie selectedCategorie;
        public Categorie SelectedCategorie
        {
            get => selectedCategorie;
            set
            {
                selectedCategorie = value;
                NotifyPropertyChanged("SelectedCategorie");
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
            categorieBLL.AddMethod(obj);
            ErrorMessage = categorieBLL.ErrorMessage;
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
            if (SelectedCategorie != null)
            {
                categorieBLL.UpdateMethod(SelectedCategorie);
                ErrorMessage = categorieBLL.ErrorMessage;
            }
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
            if (SelectedCategorie != null)
            {
                categorieBLL.DeleteMethod(SelectedCategorie);
                ErrorMessage = categorieBLL.ErrorMessage;
            }
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
