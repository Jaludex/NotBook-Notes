using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace NotBook_Notes.Views;

public partial class VerNotas : ContentPage
{
    IToast anuncio = Toast.Make("Nota Guardada");
    public VerNotas()
	{
		InitializeComponent();
		CambiarTituloContexto();
	}

	private void CambiarTituloContexto()
	{
		//Aqui se implementa el decir Ver Nota o Crear Nota Acorde al contexto
	}

    private async void BtnGuardarNota_Clicked(object sender, EventArgs e)
    {
		await anuncio.Show();
    }

    private void TituloEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
		BtnGuardarNota.IsVisible = true;
    }
}