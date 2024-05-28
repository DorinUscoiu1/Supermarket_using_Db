using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public interface IDataContext
    {
        Producator GetProducatorByName(string name);
        Categorie GetCategoriaByName(string name);
        IEnumerable<Produs> GetProduseByProducator(Producator producator);
        IEnumerable<Produs> GetProduseByCategorie(Categorie categorie);
        Stoc GetStocByProdusId(int produsId);
        IEnumerable<Stoc> GetStocuriByProdusId(int produsId);
        IEnumerable<Stoc> GetActiveStocuri();
        void AddStoc(Stoc stoc);
        void UpdateStoc(Stoc stoc);
        void DeactivateStoc(Stoc stoc);
    }
}
