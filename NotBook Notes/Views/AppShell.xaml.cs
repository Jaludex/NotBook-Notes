﻿using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using Plugin.LocalNotification;

namespace NotBook_Notes.Views
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Rutas = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            RegistrarRutas();
        }

        //Actualizar aca cada nueva ventana que se cree
        private void RegistrarRutas()
        {
            try
            {
                Rutas.Add(nameof(PaginaNotas), typeof(PaginaNotas));
                Rutas.Add(nameof(PaginaRecordatorios), typeof(PaginaRecordatorios));
                Rutas.Add(nameof(VerNotas), typeof(VerNotas));
                Rutas.Add(nameof(Papelera), typeof(Papelera));
                Rutas.Add(nameof(Opciones), typeof(Opciones));

                foreach (var ruta in Rutas)
                {
                    Routing.RegisterRoute(ruta.Key, ruta.Value);                
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error Fatal", "Ha ocurrido un error al intentar cargar la app, intentelo mas tarde", "Cerrar");
                Application.Current.Quit();
            }
        }

    }
}
