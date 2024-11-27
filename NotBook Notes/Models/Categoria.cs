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
        public String nombreCategoría { get; set; }
        public Color colorNotas { get; set; }

        public Categoria(string Nombre, Color ColorNotas) 
        {
            notas = new List<Nota>();
            this.colorNotas = ColorNotas;
            this.nombreCategoría = Nombre;
        }
    }
}
