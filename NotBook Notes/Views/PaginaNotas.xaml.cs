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

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegación a la nueva página
    }
}