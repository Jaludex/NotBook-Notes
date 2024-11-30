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
        public ObservableCollection<Nota> _notasFiltradas { get; set; }

        // Con esto vemos si cambiaron las propiedades de dichos atributos
        public ObservableCollection<Nota> notasFiltradas
        {
            get => _notasFiltradas;
            set
            {
                _notasFiltradas = value;
                OnPropertyChanged(); 
            }
        }

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


        // Esto es para filtrar las notas según lo que se escriba en la barra de busqueda
        // retorna false cuando no hay notas que coincidan con la busqueda
       
        public bool Filtrar(string criterioBusqueda, string NotaORecordatorio)
        {
            // Esto verifica si el buscador esta vacio, si lo esta ejecuta esto:
            if (string.IsNullOrEmpty(criterioBusqueda))
            {
                // Ahora verifica si estamos pidiendo notas, si es asi muestra todas las notas existentes asignandolas a la coleccion de notas filtradas
                if(NotaORecordatorio == "Nota")
                {
                    notasFiltradas = new ObservableCollection<Nota>(notas.Where(n => !(n is Recordatorio))); // Filtra solo las notas que no son de tipo Recordatorio;
                }
                else
                // Si pedimos recordatorio entonces ve si hay algun recordatorio en notas y lo asigna a notas filtradas
                {
                    notasFiltradas = new ObservableCollection<Nota>(notas.Where(n => n is Recordatorio));
                }

                if (notasFiltradas.Any())
                {
                    return false;

                } else
                {
                    return true;
                }
            }
            else
            {
                // Si hay algo escrito en el criterio de búsqueda, compara y muestra las coincidencias
                // Ver si la búsqueda coincide con alguno de los datos de la nota (título, contenido o categoría)

                var resultado = new ObservableCollection<Nota>(
                    notas.Where(n =>
                        n.Titulo.Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase) ||
                        n.Contenido.Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase) ||
                        n.Categoria.NombreCategoria.Contains(criterioBusqueda, StringComparison.OrdinalIgnoreCase))
                );
                // si es nota entonces filtra nada mas las notas
                if(NotaORecordatorio == "Nota")
                {
                    notasFiltradas = new ObservableCollection<Nota>(resultado.Where(n => !(n is Recordatorio)));
                }
                // si es recordatorio
                else
                {
                    notasFiltradas = new ObservableCollection<Nota>(resultado.Where(n => (n is Recordatorio)));
                }
                //notasFiltradas = new ObservableCollection<Nota>(resultado);

                // Si resultado no tiene nada, mostrar las notas filtradas que hayan
                if (!resultado.Any())
                {
                    return true;
                    
                }
                else
                {
                    // Luego ver como hacer que el label diga que no hay coincidencia en PaginaNotas.xaml
                    return false;
                };
               
            }
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
            OnPropertyChanged(nameof(notasFiltradas));
        }


        //Estos de aqui son exclusivamente para el contexto de uso en papelera, porque que es tedioso crear viewmodels nuevos cuando abos contienen la misma clase de objetos
        public ICommand EliminarNotaCommand => new Command<Nota>(EliminarNota);
        public ICommand RestaurarNotaCommand => new Command<Nota>(RestaurarNota);

        private void EliminarNota(Nota notaSeleccionada)
        {
            if (notaSeleccionada != null && notas.Contains(notaSeleccionada))
            {
                notas.Remove(notaSeleccionada);
                // Aquí puedes agregar lógica adicional como guardar cambios en la base de datos.
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
                string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
                ManejoDeDatos.GuardarDatosJSONAsync(ruta);
            }
        }
    }
}
