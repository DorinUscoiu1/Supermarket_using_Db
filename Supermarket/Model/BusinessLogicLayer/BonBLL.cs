using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Supermarket.ViewModel;

namespace Supermarket.Model.BusinessLogicLayer
{
    public class BonBLL
    {
        private SupermarketContext context = new SupermarketContext();

        public void SaveBon(ObservableCollection<BonProdus> bonCurent, decimal totalBon)
        {
            var bon = new Bon
            {
                SumaIncasata = totalBon,
                DataEliberare = DateTime.Now,
                CasierId = 1,
                BonProduse = new List<BonProdus>()
            };

            foreach (var bonProdus in bonCurent)
            {
                var existingProdus = context.Produse.Find(bonProdus.Produs.Id);
                if (existingProdus != null)
                {
                    bonProdus.Produs = existingProdus;
                }
                bonProdus.Bon = bon; 
                bon.BonProduse.Add(bonProdus);
            }

            context.Bonuri.Add(bon);
            context.SaveChanges();
        }


        public ObservableCollection<Bon> GetBons()
        {
            return new ObservableCollection<Bon>(
                context.Bonuri
                    .Include(b => b.BonProduse.Select(bp => bp.Produs))
                    .ToList());
        }

    }
}
