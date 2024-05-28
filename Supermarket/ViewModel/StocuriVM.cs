using System.Collections.ObjectModel;
using System.Windows.Input;
using Supermarket.Helper;
using Supermarket.Model;
using Supermarket.Model.BusinessLogicLayer;

namespace Supermarket.ViewModel
{
    public class StocuriVM : BasePropertyChanged
    {
        private StocBLL stocBLL;

        public StocuriVM()
        {
            stocBLL = new StocBLL();
            StocuriList = stocBLL.StocuriList;
            ProduseList = stocBLL.ProduseList;
        }

        public ObservableCollection<Stoc> StocuriList
        {
            get => stocBLL.StocuriList;
            set
            {
                stocBLL.StocuriList = value;
                NotifyPropertyChanged("StocuriList");
            }
        }

        public ObservableCollection<Produs> ProduseList { get; set; }

        private Stoc selectedStoc;
        public Stoc SelectedStoc
        {
            get => selectedStoc;
            set
            {
                selectedStoc = value;
                NotifyPropertyChanged("SelectedStoc");
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
            stocBLL.AddMethod(obj as Stoc);
            ErrorMessage = stocBLL.ErrorMessage;
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
            stocBLL.UpdateMethod(SelectedStoc);
            ErrorMessage = stocBLL.ErrorMessage;
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
            stocBLL.DeleteMethod(SelectedStoc);
            ErrorMessage = stocBLL.ErrorMessage;
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