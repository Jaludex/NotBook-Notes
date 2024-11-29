namespace NotBook_Notes.Views;
using NotBook_Notes.ViewModels;

public partial class PaginaNotas : ContentPage
{
	public PaginaNotas()
	{
		InitializeComponent();
        BindingContext = new NotaViewModel();
    }

    private async void BtnNuevaNota_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = false;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegaci�n a la nueva p�gina
    }

}