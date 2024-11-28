using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Recordatorio : Nota
    {
        public DateTime fechaLimite { get; set; }

        public Recordatorio(string Titulo, string Contentido, DateTime FechaCreacion, DateTime FechaLimite, int categoria) : base(Titulo, Contentido, FechaCreacion, categoria)
        {
            this.fechaLimite = FechaLimite;
        }
    }
}
