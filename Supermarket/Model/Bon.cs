using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class Bon
    {
        public int Id { get; set; }
        public DateTime DataEliberare { get; set; }
        public int CasierId { get; set; }
        public User Casier { get; set; }
        public decimal SumaIncasata { get; set; }
        public List<BonProdus> BonProduse { get; set; }
    }
}
