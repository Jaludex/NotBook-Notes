namespace NotBook_Notes.Views;

public partial class PaginaNotas : ContentPage
{
	public PaginaNotas()
	{
		InitializeComponent();
	}

    private async void BtnNuevaNota_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = false;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegaci�n a la nueva p�gina
    }
}