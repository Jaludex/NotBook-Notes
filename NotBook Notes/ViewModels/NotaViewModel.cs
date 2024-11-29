using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using NotBook_Notes.Models;
using System.Windows.Input;

namespace NotBook_Notes.ViewModels
{
    public class NotaViewModel : INotifyPropertyChanged
    {
        private Nota _nota;

        public Nota Nota
        {
            get => _nota;
            set
            {
                if (_nota != value)
                {
                    _nota = value;
                    OnPropertyChanged();  // Notificar cambio
                }
            }
        }

        public ICommand CambiarTituloCommand { get; }

        public NotaViewModel()
        {
            // Inicializa la propiedad Nota con valores predeterminados
            Nota = new Nota("Mi primera nota", "Este es el contenido de la nota.", new DateTime(2001, 2, 10), 2);

            // Inicializa el comando
            CambiarTituloCommand = new Command(CambiarTitulo);
        }

        private void CambiarTitulo()
        {
            Nota.Titulo = "Nuevo título de la nota";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
