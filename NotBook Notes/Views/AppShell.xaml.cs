using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;

namespace NotBook_Notes.Views
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Rutas = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            RegistrarRutas();
        }

        //Actualizar aca cada nueva ventana que se cree
        private void RegistrarRutas()
        {
            try
            {
                Rutas.Add(nameof(PaginaNotas), typeof(PaginaNotas));
                Rutas.Add(nameof(PaginaRecordatorios), typeof(PaginaRecordatorios));
                Rutas.Add(nameof(VerNotas), typeof(VerNotas));

                foreach (var ruta in Rutas)
                {
                    Routing.RegisterRoute(ruta.Key, ruta.Value);                
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error Fatal", "Ha ocurrido un error al intentar cargar la app, intentelo mas tarde", "Cerrar");
                Application.Current.Quit();
            }
        }

        private async void AppShell_Appearing(object sender, EventArgs e)
        {
            await DisplayAlert("pito", $"pito", "OK");
            if (string.IsNullOrEmpty(ManejoDeDatos.nombreUsuario))
            {
                try
                {
                    string username = await DisplayPromptAsync("Personalización", "Para hacer más amena nuestra interacción... ¿Por qué no me dices tu nombre?", "Aceptar", "Llamame 'Usuario'", "¿Quién Eres?", 10, Keyboard.Text);

                    if (string.IsNullOrEmpty(username))
                    {
                        username = "Usuario";
                    }

                    ManejoDeDatos.nombreUsuario = username;

                    await DisplayAlert("¡Todo Listo!", $"A partir de ahora, te llamaré {username}, te queda muy bien.{Environment.NewLine}Sin más preámbulo, disfruta la app, cualquier duda, accede a un botón ? en los menús para obtener ayuda", "¡Vamos!");
                }
                catch (Exception ex)
                {
                    // Mostrar un error si hay un problema
                    await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                }
            }
        }

    }
}
