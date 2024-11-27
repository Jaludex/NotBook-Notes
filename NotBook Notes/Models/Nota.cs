using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Nota
    {
        public string titulo { get; set; }
        public string contenido { get; set; }
        public DateTime fechaCreacion { get; set; }

        public Nota(string Titulo, string Contentido, DateTime FechaCreacion) 
        {
            this.titulo = Titulo;
            this.contenido = Contentido;
            this.fechaCreacion = FechaCreacion;
        }
    }
}
