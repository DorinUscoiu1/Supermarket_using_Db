using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Supermarket.Model.BusinessLogicLayer
{
    public class StocBLL
    {
        private SupermarketContext context;

        public ObservableCollection<Stoc> StocuriList { get; set; }
        public ObservableCollection<Produs> ProduseList { get; set; }
        public string ErrorMessage { get; set; }

        public StocBLL()
        {
            context = new SupermarketContext();
            StocuriList = new ObservableCollection<Stoc>(
                context.Stocuri.Include(s => s.Produs)
                .Where(s => s.Cantitate > 0 && s.DataExpirare > DateTime.Now)
                .ToList()
            );
            ProduseList = new ObservableCollection<Produs>(context.Produse.ToList());
        }

        public void AddMethod(object obj)
        {
            Stoc stoc = obj as Stoc;
            if (stoc != null)
            {
                if (stoc.Produs == null || stoc.ProdusId == 0)
                {
                    ErrorMessage = "Produsul trebuie precizat";
                }
                else if (stoc.Cantitate <= 0)
                {
                    ErrorMessage = "Cantitatea trebuie sa fie un numar pozitiv";
                }
                else if (stoc.PretAchizitie <= 0)
                {
                    ErrorMessage = "Pretul de achizitie trebuie sa fie un număr pozitiv";
                }
                else if (stoc.DataExpirare <= DateTime.Now)
                {
                    ErrorMessage = "Data de expirare trebuie sa fie in viitor";
                }
                else
                {
                    stoc.PretVanzare = stoc.PretAchizitie * 1.19m;
                    context.Stocuri.Add(stoc);
                    context.SaveChanges();
                    if (stoc.Cantitate > 0 && stoc.DataExpirare > DateTime.Now)
                    {
                        StocuriList.Add(stoc);
                    }
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Stoc stoc = obj as Stoc;
            if (stoc == null)
            {
                ErrorMessage = "Selecteaza un stoc";
            }
            else if (stoc.ProdusId == 0)
            {
                ErrorMessage = "Produsul trebuie precizat";
            }
            else if (stoc.Cantitate <= 0)
            {
                ErrorMessage = "Cantitatea trebuie sa fie un numar pozitiv";
            }
            else if (stoc.PretAchizitie <= 0)
            {
                ErrorMessage = "Pretul de achizitie trebuie sa fie un numar pozitiv";
            }
            else if (stoc.DataExpirare <= DateTime.Now)
            {
                ErrorMessage = "Data de expirare trebuie sa fie in viitor";
            }
            else
            {
                Stoc s = context.Stocuri.Find(stoc.Id);
                if (s != null)
                {
                    s.ProdusId = stoc.ProdusId;
                    s.Cantitate = stoc.Cantitate;
                    s.UnitateMasura = stoc.UnitateMasura;
                    s.DataAprovizionare = stoc.DataAprovizionare;
                    s.DataExpirare = stoc.DataExpirare;
                    s.PretAchizitie = stoc.PretAchizitie;
                    s.PretVanzare = stoc.PretAchizitie * 1.19m;
                    context.SaveChanges();

                    if (s.Cantitate == 0 || s.DataExpirare <= DateTime.Now)
                    {
                        StocuriList.Remove(s);
                    }
                    else
                    {
                        var index = StocuriList.IndexOf(s);
                        if (index >= 0)
                        {
                            StocuriList[index] = s;
                        }
                    }
                    ErrorMessage = "";
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            Stoc stoc = obj as Stoc;
            if (stoc == null)
            {
                ErrorMessage = "Selecteaza un stoc";
            }
            else
            {
                Stoc s = context.Stocuri.Find(stoc.Id);
                if (s != null)
                {
                    context.Stocuri.Remove(s);
                    context.SaveChanges();
                    StocuriList.Remove(s);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateStock(int produsId, decimal cantitateVanduta)
        {
            var stocuri = context.Stocuri.Where(s => s.ProdusId == produsId)
                                         .OrderBy(s => s.DataAprovizionare)
                                         .ToList();

            foreach (var stoc in stocuri)
            {
                if (cantitateVanduta <= 0)
                    break;

                if (stoc.Cantitate > cantitateVanduta)
                {
                    stoc.Cantitate -= cantitateVanduta;
                    cantitateVanduta = 0;
                }
                else
                {
                    cantitateVanduta -= stoc.Cantitate;
                    stoc.Cantitate = 0;
                }

                if (stoc.Cantitate == 0 || stoc.DataExpirare <= DateTime.Now)
                {
                    StocuriList.Remove(stoc);
                }

                context.SaveChanges();
            }
        }
    }
}