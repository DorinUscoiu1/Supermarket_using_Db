using Supermarket.Model;
using Supermarket.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Supermarket.Model
{
    public class DataContext : IDataContext
    {
        private List<Producator> Producatori { get; set; }
        private List<Categorie> Categorii { get; set; }
        private List<Produs> Produse { get; set; }
        private List<Stoc> Stocuri { get; set; }

        public DataContext()
        {
            Producatori = new List<Producator>();
            Categorii = new List<Categorie>();
            Produse = new List<Produs>();
            Stocuri = new List<Stoc>();

            Producatori.Add(new Producator { Id = 1, Nume = "Producator1" });
            Categorii.Add(new Categorie { Id = 1, Nume = "Categorie1" });
            Produse.Add(new Produs { Id = 1, Nume = "Produs1", CodBare = "1234567890123", Categoria = Categorii[0], Producator = Producatori[0] });
            Stocuri.Add(new Stoc { Id = 1, ProdusId = 1, Produs = Produse[0], Cantitate = 10, UnitateMasura = "buc", DataAprovizionare = DateTime.Now.AddMonths(-1), DataExpirare = DateTime.Now.AddMonths(6), PretAchizitie = 5.0m, PretVanzare = 8.0m, IsActive = true });
        }

        public Producator GetProducatorByName(string name)
        {
            return Producatori.FirstOrDefault(p => p.Nume == name);
        }

        public Categorie GetCategoriaByName(string name)
        {
            return Categorii.FirstOrDefault(c => c.Nume == name);
        }

        public IEnumerable<Produs> GetProduseByProducator(Producator producator)
        {
            return Produse.Where(p => p.Producator.Id == producator.Id);
        }

        public IEnumerable<Produs> GetProduseByCategorie(Categorie categorie)
        {
            return Produse.Where(p => p.Categoria.Id == categorie.Id);
        }

        public Stoc GetStocByProdusId(int produsId)
        {
            return Stocuri.FirstOrDefault(s => s.ProdusId == produsId);
        }

        public IEnumerable<Stoc> GetStocuriByProdusId(int produsId)
        {
            return Stocuri.Where(s => s.ProdusId == produsId);
        }

        public IEnumerable<Stoc> GetActiveStocuri()
        {
            return Stocuri.Where(s => s.IsActive && s.DataExpirare > DateTime.Now && s.Cantitate > 0);
        }

        public void AddStoc(Stoc stoc)
        {
            stoc.Id = Stocuri.Any() ? Stocuri.Max(s => s.Id) + 1 : 1;
            Stocuri.Add(stoc);
        }

        public void UpdateStoc(Stoc stoc)
        {
            var existingStoc = Stocuri.FirstOrDefault(s => s.Id == stoc.Id);
            if (existingStoc != null)
            {
                existingStoc.Cantitate = stoc.Cantitate;
                existingStoc.UnitateMasura = stoc.UnitateMasura;
                existingStoc.DataExpirare = stoc.DataExpirare;
                existingStoc.PretVanzare = stoc.PretVanzare;
                existingStoc.IsActive = stoc.IsActive;
            }
        }

        public void DeactivateStoc(Stoc stoc)
        {
            var existingStoc = Stocuri.FirstOrDefault(s => s.Id == stoc.Id);
            if (existingStoc != null)
            {
                existingStoc.IsActive = false;
            }
        }
    }
}
