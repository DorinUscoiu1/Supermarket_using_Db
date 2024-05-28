using System;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Input;
using Supermarket.Helper;
using Supermarket.Model;
using Supermarket.Model.BusinessLogicLayer;

namespace Supermarket.ViewModel
{
    public class ProduseVM : BasePropertyChanged
    {
        private ProdusBLL produsBLL;

        public ProduseVM()
        {
            produsBLL = new ProdusBLL();
            ProduseList = produsBLL.ProduseList;
            CategoriiList = produsBLL.CategoriiList;
            ProducatoriList = produsBLL.ProducatoriList;
        }
        public ObservableCollection<Produs> SearchResults { get; set; }

        public ObservableCollection<Produs> ProduseList
        {
            get => produsBLL.ProduseList;
            set
            {
                produsBLL.ProduseList = value;
                NotifyPropertyChanged("ProduseList");
            }
        }

        public ObservableCollection<Categorie> CategoriiList
        {
            get => produsBLL.CategoriiList;
            set
            {
                produsBLL.CategoriiList = value;
                NotifyPropertyChanged("CategoriiList");
            }
        }

        public ObservableCollection<Producator> ProducatoriList
        {
            get => produsBLL.ProducatoriList;
            set
            {
                produsBLL.ProducatoriList = value;
                NotifyPropertyChanged("ProducatoriList");
            }
        }


        private Produs selectedProdus;
        public Produs SelectedProdus
        {
            get => selectedProdus;
            set
            {
                selectedProdus = value;
                NotifyPropertyChanged("SelectedProdus");
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
            produsBLL.AddMethod(obj as Produs);
            ErrorMessage = produsBLL.ErrorMessage;
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
            produsBLL.UpdateMethod(SelectedProdus);
            ErrorMessage = produsBLL.ErrorMessage;
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
            produsBLL.DeleteMethod(SelectedProdus);
            ErrorMessage = produsBLL.ErrorMessage;
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

        public void SearchMethod(object obj)
        {
            var parameters = obj as object[];
            if (parameters != null)
            {
                string nume = parameters[0]?.ToString();
                string codBare = parameters[1]?.ToString();
                int? producatorId = parameters[2] as int?;
                int? categorieId = parameters[3] as int?;

                var results = produsBLL.SearchProducts(nume, codBare, producatorId, categorieId);
                SearchResults.Clear();
                foreach (var produs in results)
                {
                    SearchResults.Add(produs);
                }
            }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(SearchMethod);
                }
                return searchCommand;
            }
        }
    }
}
