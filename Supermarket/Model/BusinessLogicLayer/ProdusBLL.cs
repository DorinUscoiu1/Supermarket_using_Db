using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.Model.BusinessLogicLayer
{
    public class ProdusBLL
    {
        private SupermarketContext context = new SupermarketContext();
        public ObservableCollection<Produs> ProduseList { get; set; }
        public ObservableCollection<Categorie> CategoriiList { get; set; }
        public ObservableCollection<Producator> ProducatoriList { get; set; }
        public ObservableCollection<Stoc> StocList { get; set; }
        public string ErrorMessage { get; set; }

        public ProdusBLL()
        {
            ProduseList = new ObservableCollection<Produs>(context.Produse.Include(p => p.Categoria).Include(p => p.Producator).ToList());
            CategoriiList = new ObservableCollection<Categorie>(context.Categorii.ToList());
            ProducatoriList = new ObservableCollection<Producator>(context.Producatori.ToList());
            StocList = new ObservableCollection<Stoc>(context.Stocuri.ToList());
            GetProduse();
            GetCategorii();
            GetProducatori();
            GetStocuri();
        }

        public List<Produs> GetProduse()
        {
            return context.Produse.Include(p => p.Categoria).Include(p => p.Producator).ToList();
        }

        public List<Categorie> GetCategorii()
        {
            return context.Categorii.ToList();
        }

        public List<Producator> GetProducatori()
        {
            return context.Producatori.ToList();
        }

        public List<Stoc> GetStocuri()
        {
            return context.Stocuri.ToList();
        }

        public void AddMethod(object obj)
        {
            Produs produs = obj as Produs;
            if (produs != null)
            {
                if (string.IsNullOrEmpty(produs.Nume) || string.IsNullOrEmpty(produs.CodBare))
                {
                    ErrorMessage = "Toate cmpurile trebuie completate.";
                    return;
                }
                else
                {
                    context.Produse.Add(produs);
                    context.SaveChanges();
                    ProduseList.Add(produs);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Produs produs = obj as Produs;
            if (produs == null)
            {
                ErrorMessage = "Selecteaza un produs";
                return;
            }
            if (string.IsNullOrEmpty(produs.Nume) || string.IsNullOrEmpty(produs.CodBare))
            {
                ErrorMessage = "Toate campurile trebuie completate.";
                return;
            }

            Produs p = context.Produse.Find(produs.Id);
            if (p != null)
            {
                p.Nume = produs.Nume;
                p.CodBare = produs.CodBare;
                p.CategoriaId = produs.CategoriaId;
                p.ProducatorId = produs.ProducatorId;
                context.SaveChanges();
                var index = ProduseList.IndexOf(p);
                if (index >= 0)
                {
                    ProduseList[index] = p;
                }

                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Produs produs = obj as Produs;
            if (produs == null)
            {
                ErrorMessage = "Selecteaza un produs";
                return;
            }

            Produs p = context.Produse.Find(produs.Id);
            if (p != null)
            {
                context.Produse.Remove(p);
                context.SaveChanges();
                ProduseList.Remove(p);

                ErrorMessage = "";
            }
        }

        public List<Produs> SearchProducts(string nume, string codBare, int? producatorId, int? categorieId)
        {
            var query = context.Produse.Include(p => p.Categoria).Include(p => p.Producator).AsQueryable();

            if (!string.IsNullOrEmpty(nume))
            {
                query = query.Where(p => p.Nume.Contains(nume));
            }

            if (!string.IsNullOrEmpty(codBare))
            {
                query = query.Where(p => p.CodBare.Contains(codBare));
            }

            if (producatorId.HasValue)
            {
                query = query.Where(p => p.ProducatorId == producatorId.Value);
            }

            if (categorieId.HasValue)
            {
                query = query.Where(p => p.CategoriaId == categorieId.Value);
            }

            return query.ToList();
        }
        public Stoc GetStocById(int stocId)
        {
            return context.Stocuri.Find(stocId);
        }

        public void UpdateStoc(Stoc stoc)
        {
            var stocInDb = context.Stocuri.Find(stoc.Id);
            if (stocInDb != null)
            {
                stocInDb.Cantitate = stoc.Cantitate;
                context.SaveChanges();
            }
        }
        public void RemoveStoc(Stoc stoc)
        {
            var stocInDb = context.Stocuri.Find(stoc.Id);
            if (stocInDb != null)
            {
                context.Stocuri.Remove(stocInDb);
                context.SaveChanges();
            }
        }

    }

}
