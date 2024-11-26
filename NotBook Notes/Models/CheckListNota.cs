using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class CheckListNota
    {
        public List<String> Campos { get; set; }
        public String Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }

        public CheckListNota() { }
    }
}
