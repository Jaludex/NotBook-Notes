using NotBook_Notes.Models;

namespace NotBook_Notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Current!.MainPage = new Views.AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
            _ = RevisarUsernameDefault(); // Llama a la función al iniciar
        }

        private async Task RevisarUsernameDefault()
        {
            string username = ManejoDeDatos.nombreUsuario;

            if (string.IsNullOrEmpty(username))
            {
                username = await Application.Current.MainPage.DisplayPromptAsync(
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
            }
        }
    }
}
