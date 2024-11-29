using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Newtonsoft.Json;

namespace NotBook_Notes.Models
{
    public class Nota : INotifyPropertyChanged
    {
        // Campos privados tienen que seguir esta estructura
        private string _titulo;
        private string _contenido;
        private DateTime _fechaCreacion;
        private Categoria _categoria;

        // Propiedad Titulo con notificación de cambios
        [JsonProperty("titulo")]
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
        [JsonProperty("contenido")]
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
        [JsonProperty("fechaCreacion")]
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
        [JsonProperty("categoria")]
        public Categoria Categoria
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
        public Nota(string titulo, string contenido, DateTime fechaCreacion, Categoria categoria)
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