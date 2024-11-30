using NotBook_Notes.Models;

namespace NotBook_Notes.Views;

public partial class Opciones : ContentPage
{
	public Opciones()
	{
		InitializeComponent();
	}

    private async void btnCambiarNombre_Clicked(object sender, EventArgs e)
    {
        string username = await DisplayPromptAsync(
                    "Personalización",
                    "Para hacer más amena nuestra interacción... ¿Por qué no me dices tu nombre?",
                    "Aceptar",
                    "Llamame 'Usuario'",
                    "¿Quién Eres?",
                    10,
                    Keyboard.Text
                );

        ManejoDeDatos.nombreUsuario = string.IsNullOrEmpty(username) ? "Usuario" : username;

        await Application.Current.MainPage.DisplayAlert("¡Todo Listo!", $"A partir de ahora, te llamaré {ManejoDeDatos.nombreUsuario}, te queda muy bien.", "¡Vamos!");

        string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
        await ManejoDeDatos.GuardarDatosJSONAsync(ruta);
    }

    private async void btnBorrarTodo_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("¿Estas Seguro?", $"Esta accion dejara la app como nueva, eliminando tus preciadas notas{Environment.NewLine}(Te van a extrañar mucho)", "BORRAR TODO", "Cancelar");
        if (answer)
        {
            await DisplayAlert("Borrando...", "Fue un placer conocerte, la app esta por cerrarse para terminar el procedimiento", "Cerrar App");
            string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
            File.Delete(ruta);
            Application.Current.Quit();
        }
    }

    private async void btnAcercaDe_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Informacion Importante", $"App hecha como proyecto final de PR2 por el Grupo 3, en la Facultad de Ingenieria de la Universidad de los Andes {Environment.NewLine}{Environment.NewLine}Grupo:{Environment.NewLine}Rosa Rosales{Environment.NewLine}Jesús León{Environment.NewLine}Daniel Rosetti{Environment.NewLine}Jorge Carrero{Environment.NewLine}Fabian Andreth{Environment.NewLine}{Environment.NewLine}El proyecto aun no cuenta con licencias de publicacion{Environment.NewLine}{Environment.NewLine}Hecho con cariño", "Volver");
    }
}