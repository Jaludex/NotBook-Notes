using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace NotBook_Notes.Views;

public partial class VerNotas : ContentPage
{
    IToast anuncio = Toast.Make("Nota Guardada");
	bool esRecordatorio;
	DateTime fechaLimite;

    public VerNotas(bool esRecordatorio)
	{
		InitializeComponent();
		LabelFechaCreacion.Text = DateTime.Now.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
		this.esRecordatorio = esRecordatorio;
		CambiarContexto();
	}

	private void CambiarContexto()
	{
		if (esRecordatorio) 
		{
            FrameRecordatorio.IsVisible = true;
        }

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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		FechaLimite.Focus();
    }

    private void FechaLimite_DateSelected(object sender, DateChangedEventArgs e)
    {
		fechaLimite = e.NewDate;
		if (fechaLimite <= DateTime.Today)
		{
			IToast error = Toast.Make("Introduzca una Fecha Valida para Recordatorios");
			error.Show();
			fechaLimite = e.OldDate;

			return;
		}

		LabelFechaLimite.Text = "Fecha Limite: " + fechaLimite.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
    }
}