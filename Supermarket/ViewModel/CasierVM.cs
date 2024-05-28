using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Supermarket.Helper;
using Supermarket.Model;
using Supermarket.Model.BusinessLogicLayer;
using Supermarket.View;

namespace Supermarket.ViewModel
{
    public class CasierVM : BasePropertyChanged
    {
        private ProdusBLL produsBLL;
        private BonBLL bonBLL;
        public ICommand ViewBonsCommand { get; }

        public CasierVM()
        {
            produsBLL = new ProdusBLL();
            bonBLL = new BonBLL();
            ProduseList = new ObservableCollection<Produs>(produsBLL.GetProduse());
            CategoriiList = new ObservableCollection<Categorie>(produsBLL.GetCategorii());
            ProducatoriList = new ObservableCollection<Producator>(produsBLL.GetProducatori());
            SearchResults = new ObservableCollection<Tuple<Produs, decimal, decimal, DateTime>>();
            BonCurent = new ObservableCollection<BonProdus>();
            StocList = new ObservableCollection<Stoc>(produsBLL.GetStocuri());
            BonsList = new ObservableCollection<Bon>(bonBLL.GetBons());
            ViewBonsCommand = new RelayCommand(_ => ViewBons());
        }
        public ObservableCollection<Produs> ProduseList { get; set; }
        public ObservableCollection<Categorie> CategoriiList { get; set; }
        public ObservableCollection<Producator> ProducatoriList { get; set; }
        public ObservableCollection<Tuple<Produs, decimal, decimal, DateTime>> SearchResults { get; set; }

        public ObservableCollection<BonProdus> BonCurent { get; set; }
        public ObservableCollection<Stoc> StocList { get; set; }
        public ObservableCollection<Bon> BonsList { get; set; }

        public string NumeCasier { get; set; }

        private string nume;
        public string Nume
        {
            get => nume;
            set
            {
                nume = value;
                NotifyPropertyChanged(nameof(Nume));
            }
        }
        private DateTime dataExpirarii;
        public DateTime DataExpirarii
        {
            get => dataExpirarii;
            set
            {
                dataExpirarii = value;
                NotifyPropertyChanged(nameof(DataExpirarii));
            }
        }
        private Tuple<Produs, decimal, decimal, DateTime> selectedProduct;
        public Tuple<Produs, decimal, decimal, DateTime> SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
            }
        }
        private Bon selectedBon;
        public Bon SelectedBon
        {
            get => selectedBon;
            set
            {
                selectedBon = value;
                NotifyPropertyChanged(nameof(SelectedBon));
            }
        }



        private int cantitateSelectata;
        public int CantitateSelectata
        {
            get => cantitateSelectata;
            set
            {
                cantitateSelectata = value;
                NotifyPropertyChanged(nameof(CantitateSelectata));
            }
        }

        private string codBare;
        public string CodBare
        {
            get => codBare;
            set
            {
                codBare = value;
                NotifyPropertyChanged(nameof(CodBare));
            }
        }

        private int? producatorId;
        public int? ProducatorId
        {
            get => producatorId;
            set
            {
                producatorId = value;
                NotifyPropertyChanged(nameof(ProducatorId));
            }
        }

        private int? categorieId;
        public int? CategorieId
        {
            get => categorieId;
            set
            {
                categorieId = value;
                NotifyPropertyChanged(nameof(CategorieId));
            }
        }

        private decimal totalBon;
        public decimal TotalBon
        {
            get => totalBon;
            set
            {
                totalBon = value;
                NotifyPropertyChanged(nameof(TotalBon));
            }
        }
        private decimal pretStoc;
        public decimal PretStoc
        {
            get => pretStoc;
            set
            {
                pretStoc = value;
                NotifyPropertyChanged(nameof(PretStoc));
            }
        }

        private decimal cantitateDisponibila;
        public decimal CantitateDisponibila
        {
            get => cantitateDisponibila;
            set
            {
                cantitateDisponibila = value;
                NotifyPropertyChanged(nameof(CantitateDisponibila));
            }
        }
        public void SearchMethod()
        {
            var results = produsBLL.SearchProducts(Nume, CodBare, ProducatorId, CategorieId);
            SearchResults.Clear();
            foreach (var produs in results)
            {
                var stoc = StocList.FirstOrDefault(s => s.ProdusId == produs.Id);
                if (stoc != null)
                {
                    CantitateDisponibila = stoc.Cantitate;
                    PretStoc = stoc.PretVanzare;
                    DataExpirarii = stoc.DataExpirare;
                }
                SearchResults.Add(new Tuple<Produs, decimal, decimal, DateTime>(produs, PretStoc, CantitateDisponibila,DataExpirarii));
            }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(param => SearchMethod());
                }
                return searchCommand;
            }
        }

        public void AddToBon(Produs produs, int cantitate)
        {
            if (produs == null || cantitate <= 0) return;

            var stoc = StocList.FirstOrDefault(s => s.ProdusId == produs.Id);

            if (stoc == null || stoc.Cantitate < cantitate)
            {
                return;
            }

            var existingBonProdus = BonCurent.FirstOrDefault(bp => bp.Produs.Id == produs.Id);
            if (existingBonProdus != null)
            {
                existingBonProdus.Cantitate += cantitate;
                existingBonProdus.Subtotal = existingBonProdus.Cantitate * stoc.PretVanzare;
            }
            else
            {
                BonCurent.Add(new BonProdus
                {
                    Produs = produs,
                    Cantitate = cantitate,
                    Subtotal = cantitate * stoc.PretVanzare
                });
            }
            stoc.Cantitate -= cantitate;
            var stocInDb = produsBLL.GetStocById(stoc.Id);
            if (stocInDb != null)
            {
                stocInDb.Cantitate = stoc.Cantitate;
                produsBLL.UpdateStoc(stocInDb);
            }
            NotifyPropertyChanged(nameof(StocList));
            CalculateTotal();
        }


        private ICommand addToBonCommand;
        public ICommand AddToBonCommand
        {
            get
            {
                if (addToBonCommand == null)
                {
                    addToBonCommand = new RelayCommand(param =>
                    {
                        if (SelectedProduct != null)
                        {
                            AddToBon(SelectedProduct.Item1, CantitateSelectata);
                        }
                    });
                }
                return addToBonCommand;
            }
        }

        private ICommand finalizeBonCommand;
        public ICommand FinalizeBonCommand
        {
            get
            {
                if (finalizeBonCommand == null)
                {
                    finalizeBonCommand = new RelayCommand(param => FinalizeBon());
                }
                return finalizeBonCommand;
            }
        }

        private void CalculateTotal()
        {
            TotalBon = BonCurent.Sum(p => p.Subtotal);
        }

        private void FinalizeBon()
        {
            if (BonCurent.Count > 0)
            {
                bonBLL.SaveBon(BonCurent, TotalBon);
                BonsList.Clear();
                foreach (var bon in bonBLL.GetBons())
                {
                    BonsList.Add(bon);
                }
                BonCurent.Clear();
                TotalBon = 0;
                NotifyPropertyChanged(nameof(BonCurent));
                NotifyPropertyChanged(nameof(BonsList));
            }
        }


        private void ViewBons()
        {
            BonsList.Clear();
            foreach (var bon in bonBLL.GetBons())
            {
                BonsList.Add(bon);
            }
            BonsWindow bonsWindow = new BonsWindow();
            bonsWindow.DataContext = this;
            bonsWindow.Show();
        }

    }
}