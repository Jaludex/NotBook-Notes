using CommunityToolkit.Maui.Views;
using System;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using NotBook_Notes.Models;
using static System.Net.Mime.MediaTypeNames;

namespace NotBook_Notes.Views
{
    public partial class MenuPopup : Popup
    {
        public MenuPopup()
        {
            InitializeComponent();
        }

        //ROSA
        //Para estos casos, si quiero que ordenes a notas, el general, no el filtrados, ordena a notas por estos criterios y ya, para que al filtrar ya esten ordenadas, no se si me explico


        private void OnOrdenarPorNombreClicked(object sender, EventArgs e)
        {
            // Lógica para ordenar por nombre
            ManejoDeDatos.ordenSeleccionado = "Nombre";
            ManejoDeDatos.OrdenarPorNombreOFecha();
            Close();
        }

        private void OnOrdenarPorFechaClicked(object sender, EventArgs e)
        {
            // Lógica para ordenar por fecha
            ManejoDeDatos.ordenSeleccionado = "Fecha";
            ManejoDeDatos.OrdenarPorNombreOFecha();
            Close();
        }        
        
        private void Opcion3_Clicked(object sender, EventArgs e)
        {
            ManejoDeDatos.ordenSeleccionado = "FechaLimite";
            ManejoDeDatos.OrdenarPorNombreOFecha();
            Close();
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Close();
        }


    }
}
