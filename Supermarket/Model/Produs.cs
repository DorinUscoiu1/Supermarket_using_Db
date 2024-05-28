using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model
{
    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string CodBare { get; set; }
        public int CategoriaId { get; set; }
        public Categorie Categoria { get; set; }
        public int ProducatorId { get; set; }
        public Producator Producator { get; set; }
        public bool IsActive { get; set; }
    }
}
