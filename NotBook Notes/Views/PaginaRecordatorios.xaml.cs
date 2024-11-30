using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;

namespace NotBook_Notes.Views;

public partial class PaginaRecordatorios : ContentPage
{
    MenuPopup menuPopupOrganizar;

    public PaginaRecordatorios()
    {
        InitializeComponent();
        InitializeMenuPopup();
        BindingContext = ManejoDeDatos.notaViewModel;

    }

    private void InitializeMenuPopup()
    {
        menuPopupOrganizar = new MenuPopup();
        menuPopupOrganizar.paraRecordatorios();
    }

    private async void BtnNuevoRecordatorio_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = true;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegación a la nueva página

    }

    protected override void OnAppearing() // Este método se llama cada vez que la página está a punto de aparecer.
    {
        base.OnAppearing();
        string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
        ManejoDeDatos.GuardarDatosJSONAsync(ruta);
        // esto es para que aparezcan todas las notas apenas abrimos la app
        if (ManejoDeDatos.Filtrar("", textoBusqueda.AutomationId))
        {
            noSeEncontro.IsVisible = false;
        }
        else
        {
            noSeEncontro.IsVisible = false;
        }
        ManejoDeDatos.OrdenarPorNombreOFecha();
        ManejoDeDatos.notaViewModel.ActualizarNotas();
    }

    private void textoBusqueda_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ManejoDeDatos.Filtrar(textoBusqueda.Text, textoBusqueda.AutomationId))
        {
            noSeEncontro.IsVisible = true;
            //solo queremos mostrar los elementos coincidentes
        }
        else
        {
            noSeEncontro.IsVisible = false;
        }
    }

    private async void btnOrdenarNotas_Clicked(object sender, EventArgs e)
    {
        await PopupExtensions.ShowPopupAsync<MenuPopup>(this, menuPopupOrganizar);
        ManejoDeDatos.notaViewModel.ActualizarNotas();

        //Llama a filtrar vacio, que saliendo de aqui ya deberia de estar ordenado, o si no, pues que lo haga y quede como estaba

        //Hay que volverlo a construir porque el solito se destruye al mostrarse, por alguna razon
        InitializeMenuPopup();
    }

    private void btnCriterio_Clicked(object sender, EventArgs e)
    {
        ManejoDeDatos.esDescendente = (ManejoDeDatos.esDescendente == false);

        if (ManejoDeDatos.esDescendente) { btnCriterio.Text = "Descendente"; }
        else { btnCriterio.Text = "Ascendente"; }
        ManejoDeDatos.OrdenarPorNombreOFecha();
        ManejoDeDatos.notaViewModel.ActualizarNotas();

        //Aqui llamamos a ordenar de nuevo, diciendole con la variable si se ordena descendente o ascendente
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (ManejoDeDatos.filtroCategoria == null)
        {
            ManejoDeDatos.filtroCategoria = ManejoDeDatos.categorias[0];
        }
        else if (ManejoDeDatos.filtroCategoria == ManejoDeDatos.categorias.Last())
        {
            ManejoDeDatos.filtroCategoria = null;

            btnFiltroCategoria.Color = Colors.White;
            //Cuadro de sin filtro
            if (ManejoDeDatos.Filtrar("", textoBusqueda.AutomationId))
            {
                noSeEncontro.IsVisible = false;
            }
            else
            {
                noSeEncontro.IsVisible = false;
            }
            ManejoDeDatos.OrdenarPorNombreOFecha();
            ManejoDeDatos.notaViewModel.ActualizarNotas();
            return;
        }
        else
        {
            ManejoDeDatos.filtroCategoria = ManejoDeDatos.categorias[ManejoDeDatos.categorias.IndexOf(ManejoDeDatos.filtroCategoria) + 1];
        }

        btnFiltroCategoria.Color = ManejoDeDatos.filtroCategoria.ColorNotas;
        if (ManejoDeDatos.Filtrar("", textoBusqueda.AutomationId))
        {
            noSeEncontro.IsVisible = false;
        }
        else
        {
            noSeEncontro.IsVisible = false;
        }
        ManejoDeDatos.OrdenarPorNombreOFecha();
        ManejoDeDatos.notaViewModel.ActualizarNotas();
    }
}