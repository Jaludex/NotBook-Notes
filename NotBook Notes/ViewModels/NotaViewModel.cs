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
using NotBook_Notes.Views;
using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;

namespace NotBook_Notes.ViewModels
{
    public class NotaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Nota> _notas { get; set; }

        public ObservableCollection<Nota> notas
        {
            get => _notas;
            set
            {
                _notas = value;
                OnPropertyChanged();
            }
        }

        public void AddNota(Nota nuevaNota)
        {
            notas.Add(nuevaNota);
            OnPropertyChanged();
        }

        public NotaViewModel()
        {
            notas = new ObservableCollection<Nota>();
        }

        public int NumeroDeColumnas
        {
            get
            {
                return notas.Count;
            }
        }

        //Busca una nota, si la encuentra suelta la posicion, si no, -1
        //Ya que observablecolllection no tiene metodos .find como las listas
        public int EncontrarNota(string titulo)
        {
            if (notas == null || notas.Count == 0)
            { return -1; }

            for (int i = 0; i < titulo.Length; i++)
            {
                if (notas[i].Titulo == titulo)
                {
                    return i;
                }
            }

            return -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand OpcionesNotaCommand => new Command<Nota>(ObtenerOpcionesNota);

        // Método que se llama al hacer clic
        private void ObtenerOpcionesNota(Nota notaSeleccionada)
        {
            // Encuentra la posición de la nota seleccionada
            int posicion = notas.IndexOf(notaSeleccionada);

            if (posicion >= 0)
            {
                AppShell.Current.Navigation.PushAsync(new VerNotas(notaSeleccionada is Recordatorio, posicion));
            }
        }

        public void ActualizarNotas()
        {
            OnPropertyChanged(nameof(notas));
        }

    }
}
