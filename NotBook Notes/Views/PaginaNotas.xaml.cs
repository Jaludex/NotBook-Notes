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
    bool descendente;
    Categoria? filtroCategoria;

	public PaginaNotas()
	{
		InitializeComponent();
        InitializeMenuPopup();
        descendente = true;
        filtroCategoria = null;
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
        if (ManejoDeDatos.FiltrarNotas(""))
        {
            noSeEncontro.IsVisible = false;
        }
        else
        {
            noSeEncontro.IsVisible = true;
        }
        ManejoDeDatos.notaViewModel.ActualizarNotas();
    }

    private void textoBusqueda_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ManejoDeDatos.FiltrarNotas(textoBusqueda.Text))
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

        //Llama a filtrar vacio, que saliendo de aqui ya deberia de estar ordenado, o si no, pues que lo haga y quede como estaba

        //Hay que volverlo a construir porque el solito se destruye al mostrarse, por alguna razon
        menuPopupOrganizar = new MenuPopup();
    }


    //Toma la coleccion que ya se filtro con los metodo anteriores, nada mas que la ordena de acuerdo al criterio
    private void OrdenarNotas(string criterio, bool orden)
    {
        // Implementar la lógica para ordenar las notas
    }

    private void btnCriterio_Clicked(object sender, EventArgs e)
    {
        descendente = (descendente == false);

        if (descendente) { btnCriterio.Text = "Descendente"; }
        else { btnCriterio.Text = "Ascendente"; }

        //Aqui llamamos a ordenar de nuevo, diciendole con la variable si se ordena desdecente o ascendente
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (filtroCategoria == null)
        {
            filtroCategoria = ManejoDeDatos.categorias[0];
        }
        else if (filtroCategoria == ManejoDeDatos.categorias.Last())
        {
            filtroCategoria = null;

            btnFiltroCategoria.Color = Colors.Transparent;
            //Cuadro de sin filtro
            //Llamar a filtrar sin criterio de categoria
            return;
        }
        else
        {
            filtroCategoria = ManejoDeDatos.categorias[ManejoDeDatos.categorias.IndexOf(filtroCategoria) + 1];
        }

        btnFiltroCategoria.Color = filtroCategoria.ColorNotas;

        //Cuadro de filtrando por X categoria
        //Llamar a filtrar con ese criterio
    }
}