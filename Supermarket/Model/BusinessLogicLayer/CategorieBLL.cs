using System.Collections.ObjectModel;
using System.Linq;

namespace Supermarket.Model.BusinessLogicLayer
{
    public class CategorieBLL
    {
        private SupermarketContext context = new SupermarketContext();
        public ObservableCollection<Categorie> CategoriiList { get; set; }
        public string ErrorMessage { get; set; }

        public CategorieBLL()
        {
            CategoriiList = new ObservableCollection<Categorie>(context.Categorii.ToList());
        }

        public void AddMethod(object obj)
        {
            Categorie categorie = obj as Categorie;
            if (categorie != null)
            {
                if (string.IsNullOrEmpty(categorie.Nume))
                {
                    ErrorMessage = "Numele categoriei trebuie precizat";
                }
                else
                {
                    context.Categorii.Add(categorie);
                    context.SaveChanges();
                    CategoriiList.Add(categorie);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Categorie categorie = obj as Categorie;
            if (categorie == null)
            {
                ErrorMessage = "Selecteaza o categorie";
            }
            else if (string.IsNullOrEmpty(categorie.Nume))
            {
                ErrorMessage = "Numele categoriei trebuie precizat";
            }
            else
            {
                Categorie c = context.Categorii.Find(categorie.Id);
                if (c != null)
                {
                    c.Nume = categorie.Nume;
                    context.SaveChanges();
                    ErrorMessage = "";
                }
            }
        }

        public void DeleteMethod(object obj)
        {
            Categorie categorie = obj as Categorie;
            if (categorie == null)
            {
                ErrorMessage = "Selecteaza o categorie";
            }
            else
            {
                Categorie c = context.Categorii.Find(categorie.Id);
                if (c != null)
                {
                    context.Categorii.Remove(c);
                    context.SaveChanges();
                    CategoriiList.Remove(categorie);
                    ErrorMessage = "";
                }
            }
        }
    }
}
