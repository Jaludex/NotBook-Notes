using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    internal class Recordatorio : Nota
    {
        public DateTime fechaLimite { get; set; }

        public Recordatorio(string Titulo, string Contentido, DateTime FechaCreacion, DateTime FechaLimite) : base(Titulo, Contentido, FechaCreacion)
        {
            this.fechaLimite = FechaLimite;
        }
    }
}
