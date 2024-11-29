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
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace NotBook_Notes.ViewModels
{
    public class NotaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Nota> _notas { get; set; }
        public ObservableCollection<Nota> notasFiltradas { get; set; }

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


        //Estos de aqui son exclusivamente para el contexto de uso en papelera, porque que ladilla crear viewmodels nuevos cuando abos contienen la misma clase de objetos
        public ICommand EliminarNotaCommand => new Command<Nota>(EliminarNota);
        public ICommand RestaurarNotaCommand => new Command<Nota>(RestaurarNota);

        private void EliminarNota(Nota notaSeleccionada)
        {
            if (notaSeleccionada != null && notas.Contains(notaSeleccionada))
            {
                notas.Remove(notaSeleccionada);
                // Aquí puedes agregar lógica adicional como guardar cambios en la base de datos.
                Console.WriteLine($"Nota eliminada: {notaSeleccionada.Titulo}");
                string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
                ManejoDeDatos.GuardarDatosJSONAsync(ruta);
            }
        }

        private void RestaurarNota(Nota notaSeleccionada)
        {
            if (notaSeleccionada != null)
            {
                // Aquí puedes implementar la lógica para restaurar la nota
                // Por ejemplo, agregarla a la lista de notas activas
                notas.Remove(notaSeleccionada); // Eliminar de la papelera
                                                // Agregar a la colección de notas activos si tienes uno
                ManejoDeDatos.notaViewModel.AddNota(notaSeleccionada); // Ajusta según tu lógica
                Console.WriteLine($"Nota restaurada: {notaSeleccionada.Titulo}");
                string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
                ManejoDeDatos.GuardarDatosJSONAsync(ruta);
            }
        }
    }
}
