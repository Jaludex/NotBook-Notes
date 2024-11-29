using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using NotBook_Notes.Models;

namespace NotBook_Notes.ViewModels
{
    public class NotaViewModel
    {
       public Nota Nota { get; set; }

       public NotaViewModel()
       {
         Nota = new Nota("Mi primera nota", "Este es el contenido de la nota.", new DateTime(2001, 2, 10), 2);
       }
        CambiarTituloCommand = new Command(CambiarTitulo);

        private void CambiarTitulo()
        {
            Nota.Titulo = "Nuevo título de la nota";
        }
    }
}
