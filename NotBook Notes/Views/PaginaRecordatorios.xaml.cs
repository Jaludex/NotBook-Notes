using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using System.Collections.ObjectModel;

namespace NotBook_Notes.Views;

public partial class PaginaRecordatorios : ContentPage
{
	public PaginaRecordatorios()
	{
		InitializeComponent();
        BindingContext = ManejoDeDatos.notaViewModel;
    }

    protected override void OnAppearing() // Este método se llama cada vez que la página está a punto de aparecer.
    {
        base.OnAppearing();
        // esto es para que aparezcan todas las notas apenas abrimos la app
        if (ManejoDeDatos.Filtrar("", textoBusqueda.AutomationId))
        {
            noSeEncontro.IsVisible = false;
        }
        else
        {
            noSeEncontro.IsVisible = false;
        }
        ManejoDeDatos.notaViewModel.ActualizarNotas();
    }



    private async void BtnNuevoRecordatorio_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = true;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegación a la nueva página
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
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
}