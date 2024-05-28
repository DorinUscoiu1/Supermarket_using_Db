using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class BonProdus
    {
        public int BonId { get; set; }
        public Bon Bon { get; set; }
        public int ProdusId { get; set; }
        public Produs Produs { get; set; }
        public decimal Cantitate { get; set; }
        public decimal Subtotal { get; set; }
    }
}
