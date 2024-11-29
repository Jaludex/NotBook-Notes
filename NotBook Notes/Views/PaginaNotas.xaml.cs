namespace NotBook_Notes.Views;

using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;

public partial class PaginaNotas : ContentPage
{

	public PaginaNotas()
	{
		InitializeComponent();

        BindingContext = ManejoDeDatos.notaViewModel;
    }

    private async void BtnNuevaNota_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = false;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegaci�n a la nueva p�gina
        
    }

    protected override void OnAppearing() // Este m�todo se llama cada vez que la p�gina est� a punto de aparecer.
    {
        base.OnAppearing();
        string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
        ManejoDeDatos.GuardarDatosJSONAsync(ruta);
        ManejoDeDatos.notaViewModel.ActualizarNotas();
    }
}