namespace NotBook_Notes.Views;

public partial class PaginaRecordatorios : ContentPage
{
	public PaginaRecordatorios()
	{
		InitializeComponent();
	}

    private async void BtnNuevoRecordatorio_Clicked(object sender, EventArgs e)
    {
        bool esRecordatorio = true;
        var verNotasPage = new VerNotas(esRecordatorio);

        await AppShell.Current.Navigation.PushAsync(verNotasPage); // Navegaci�n a la nueva p�gina
    }
}