using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NotBook_Notes.ViewModels
{
    class NotaViewModel : INotifyPropertyChanged
    {
        public string _Titulo;
        public string _Contenido;
        public DateTime _FechaCreacion;

        public string Contenido
        {
            get => _Contenido;
            set
            {
                if (_Contenido != value)
                {
                    _Contenido = value;
                    OnPropertyChanged(nameof(Contenido));  // Notifica a la vista sobre el cambio
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
