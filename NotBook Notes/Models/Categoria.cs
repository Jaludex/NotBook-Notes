using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Categoria
    {
        public List<Nota> notas { get; set; }
        public String NombreCategoría { get; set; }
        public Color ColorNotas { get; set; }

        public Categoria() { }
    }
}
