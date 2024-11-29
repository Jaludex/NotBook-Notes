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

namespace NotBook_Notes.ViewModels
{
    public class NotaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Nota> notas { get; set; }

        public NotaViewModel()
        {
            //notas = new ObservableCollection<Nota>(); 
            // Inicializando la colección con algunas notas de ejemplo
            notas = new ObservableCollection<Nota>
            {
                new Nota("Nota 1", "Contenido de la primera nota", DateTime.Now, 1),
                new Nota("Nota 2", "Contenido de la segunda nota", DateTime.Now.AddDays(1), 2),
                new Nota("Nota 3", "Contenido de la tercera nota", DateTime.Now.AddDays(2), 3),
                new Nota("Nota 4", "Contenido de la primera nota", DateTime.Now, 1),
                new Nota("Nota 5", "Contenido de la segunda nota", DateTime.Now.AddDays(1), 2),
                new Nota("Nota 6", "Contenido de la tercera nota", DateTime.Now.AddDays(2), 3),
            };
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

        //Esto se va a encargar de abrir la nota para visualizar y/o editar
        public ICommand NotaSeleccionadaCommand => new Command<Nota>(AbrirNota);

        private async void AbrirNota(Nota nota)
        {
            int encontrado = EncontrarNota(nota.Titulo);
            if (nota != null && encontrado != -1)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new VerNotas(nota is Recordatorio, encontrado));
            }
        }

    }
}
