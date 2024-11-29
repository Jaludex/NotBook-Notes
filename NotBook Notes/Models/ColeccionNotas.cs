using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    public static class ColeccionNotas
    {
        public static ObservableCollection<Nota> notas { get; set; } = new ObservableCollection<Nota>();

        public static void AgregarNota(Nota nuevaNota)
        {
            // Esto automáticamente notifica a la UI
            
            notas.Add(nuevaNota); 
        }
    }
}
