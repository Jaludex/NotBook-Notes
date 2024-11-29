using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Categoria
    {
        public String NombreCategoria { get; set; }
        public Color ColorNotas { get; set; }

        public Categoria(string Nombre, Color ColorNotas) 
        {
            this.ColorNotas = ColorNotas;
            this.NombreCategoria = Nombre;
        }
    }
}
