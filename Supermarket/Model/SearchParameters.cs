using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class SearchParameters
    {
        public string Nume { get; set; }
        public string CodBare { get; set; }
        public DateTime? DataExpirare { get; set; }
        public int? ProducatorId { get; set; }
        public int? CategorieId { get; set; }
    }
}
