namespace NotBook_Notes.Views;

public partial class PaginaNotas : ContentPage
{
	public PaginaNotas()
	{
		InitializeComponent();
	}

    private async void BtnNuevaNota_Clicked(object sender, EventArgs e)
    {
		await AppShell.Current.GoToAsync(nameof(VerNotas));
    }
}