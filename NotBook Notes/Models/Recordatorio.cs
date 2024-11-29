using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public class Recordatorio : Nota
    {
        private DateTime _fechaLimite { get; set; }

        [JsonProperty("fechaLimite")]
        public DateTime fechaLimite
        {
            get => _fechaLimite;
            set
            {
                if (_fechaLimite != value)
                {
                    _fechaLimite = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }


        public Recordatorio(string Titulo, string Contentido, DateTime FechaCreacion, DateTime FechaLimite, int categoria) : base(Titulo, Contentido, FechaCreacion, categoria)
        {
            this.fechaLimite = FechaLimite;
        }
    }
}
