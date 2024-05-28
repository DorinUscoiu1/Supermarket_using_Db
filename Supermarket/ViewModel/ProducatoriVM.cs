using Supermarket.Helper;
using Supermarket.Model.BusinessLogicLayer;
using Supermarket.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Supermarket.ViewModel
{
    public class ProducatoriVM : BasePropertyChanged
    {
        private ProducatorBLL producatorBLL;

        public ProducatoriVM()
        {
            producatorBLL = new ProducatorBLL();
            ProducatoriList = producatorBLL.ProducatoriList;
        }

        public ObservableCollection<Producator> ProducatoriList
        {
            get => producatorBLL.ProducatoriList;
            set
            {
                producatorBLL.ProducatoriList = value;
                NotifyPropertyChanged("ProducatoriList");
            }
        }

        private Producator selectedProducator;
        public Producator SelectedProducator
        {
            get => selectedProducator;
            set
            {
                selectedProducator = value;
                NotifyPropertyChanged("SelectedProducator");
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
            producatorBLL.AddMethod(obj);
            ErrorMessage = producatorBLL.ErrorMessage;
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
            producatorBLL.UpdateMethod(obj);
            ErrorMessage = producatorBLL.ErrorMessage;
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
            producatorBLL.DeleteMethod(obj);
            ErrorMessage = producatorBLL.ErrorMessage;
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
