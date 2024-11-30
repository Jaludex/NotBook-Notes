using CommunityToolkit.Maui.Views;
using System;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;

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
            // L�gica para ordenar por nombre
            Close();
        }

        private void OnOrdenarPorFechaClicked(object sender, EventArgs e)
        {
            // L�gica para ordenar por fecha
            Close();
        }

        private void OnFiltrarPorCategoriaClicked(object sender, EventArgs e)
        {
            // L�gica para filtrar por categor�a
            Close();
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
