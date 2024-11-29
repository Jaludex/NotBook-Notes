using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotBook_Notes.Models
{
    public class Nota : INotifyPropertyChanged
    {
        // Campos privados tienen que seguir esta estructura
        private string _titulo;
        private string _contenido;
        private DateTime _fechaCreacion;
        private int _categoria;

        // Propiedad Titulo con notificación de cambios
        public string Titulo
        {
            get => _titulo;
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }

        // Propiedad Contenido con notificación de cambios
        public string Contenido
        {
            get => _contenido;
            set
            {
                if (_contenido != value)
                {
                    _contenido = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }

        // Propiedad FechaCreacion con notificación de cambios
        public DateTime FechaCreacion
        {
            get => _fechaCreacion;
            set
            {
                if (_fechaCreacion != value)
                {
                    _fechaCreacion = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }

        // Propiedad Categoria con notificación de cambios
        public int Categoria
        {
            get => _categoria;
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }

        // Constructor
        public Nota(string titulo, string contenido, DateTime fechaCreacion, int categoria)
        {
            Titulo = titulo;
            Contenido = contenido;
            FechaCreacion = fechaCreacion;
            Categoria = categoria;
        }

        // Implementación de la interfaz INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para notificar a la vista que una propiedad ha cambiado
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}