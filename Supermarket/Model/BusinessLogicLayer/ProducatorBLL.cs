using System.Collections.ObjectModel;
using System.Linq;

namespace Supermarket.Model.BusinessLogicLayer
{
    public class ProducatorBLL
    {
        private SupermarketContext context = new SupermarketContext();
        public ObservableCollection<Producator> ProducatoriList { get; set; }
        public string ErrorMessage { get; set; }

        public ProducatorBLL()
        {
            ProducatoriList = new ObservableCollection<Producator>(context.Producatori.ToList());
        }

        public void AddMethod(object obj)
        {
            Producator producator = obj as Producator;
            if (producator != null)
            {
                if (string.IsNullOrEmpty(producator.Nume))
                {
                    ErrorMessage = "Numele producatorului trebuie precizat";
                }
                else if (string.IsNullOrEmpty(producator.TaraOrigine))
                {
                    ErrorMessage = "Tara de origine trebuie precizată";
                }
                else
                {
                    context.Producatori.Add(producator);
                    context.SaveChanges();
                    ProducatoriList.Add(producator);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Producator producator = obj as Producator;
            if (producator == null)
            {
                ErrorMessage = "Selecteaza un producator";
                return;
            }

            Producator p = context.Producatori.Find(producator.Id);
            if (p != null)
            {
                p.Nume = producator.Nume;
                p.TaraOrigine = producator.TaraOrigine;
                context.SaveChanges();
                var index = ProducatoriList.IndexOf(p);
                if (index >= 0)
                {
                    ProducatoriList[index] = p;
                }

                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Producator producator = obj as Producator;
            if (producator == null)
            {
                ErrorMessage = "Selecteaza un producator";
                return;
            }

            Producator p = context.Producatori.Find(producator.Id);
            if (p != null)
            {
                context.Producatori.Remove(p);
                context.SaveChanges();
                ProducatoriList.Remove(p);
                ErrorMessage = "";
            }
        }
    }
}
