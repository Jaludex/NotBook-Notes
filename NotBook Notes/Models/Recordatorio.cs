using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public Recordatorio(string Titulo, string Contentido, DateTime FechaCreacion, DateTime FechaLimite, Categoria categoria) : base(Titulo, Contentido, FechaCreacion, categoria)
        {
            this.fechaLimite = FechaLimite;
        }

        // -------------- DataBinding --------------

        // Implementación de la interfaz INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para notificar a la vista que una propiedad ha cambiado
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
