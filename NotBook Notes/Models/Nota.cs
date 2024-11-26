using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Nota
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreación { get; set; }

        public Nota() { }
    }
}
