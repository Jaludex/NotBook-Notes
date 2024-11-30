namespace NotBook_Notes.Views;

using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using System.Collections.ObjectModel;
using Android;
using AndroidX.AppCompat.View.Menu;
using CommunityToolkit.Maui.Views;

public partial class PaginaNotas : ContentPage
{
    MenuPopup menuPopupOrganizar;

	public PaginaNotas()
	{
		InitializeComponent();
        InitializeMenuPopup();
        BindingContext = ManejoDeDatos.notaViewModel;
        
    }

    private void InitializeMenuPopup()
    {
        menuPopupOrganizar = new MenuPopup();
    }

    private async void BtnNuevaNota_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = false;
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
        } else
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
        menuPopupOrganizar = new MenuPopup();
    }

    private void btnCriterio_Clicked(object sender, EventArgs e)
    {
        ManejoDeDatos.esDescendente = (ManejoDeDatos.esDescendente == false);

        if (ManejoDeDatos.esDescendente) { btnCriterio.Text = "Descendente";}
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

            btnFiltroCategoria.Color = Colors.Transparent;
            //Cuadro de sin filtro
            //Llamar a filtrar sin criterio de categoria
            return;
        }
        else
        {
            ManejoDeDatos.filtroCategoria = ManejoDeDatos.categorias[ManejoDeDatos.categorias.IndexOf(ManejoDeDatos.filtroCategoria) + 1];
        }

        btnFiltroCategoria.Color = ManejoDeDatos.filtroCategoria.ColorNotas;
        ManejoDeDatos.OrdenarPorNombreOFecha();
        ManejoDeDatos.notaViewModel.ActualizarNotas();

        //Cuadro de filtrando por X categoria
        //Llamar a filtrar con ese criterio
    }
}