using NotBook_Notes.Models;
using NotBook_Notes.Views;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

namespace NotBook_Notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
            Current!.MainPage = new Views.AppShell();
        }

        private async Task RequestPermissions()
        {
            // Solicitar permiso para enviar notificaciones
            var statusNotification = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
            if (statusNotification != PermissionStatus.Granted)
            {
                statusNotification = await Permissions.RequestAsync<Permissions.PostNotifications>();
            }

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

                string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
                await ManejoDeDatos.GuardarDatosJSONAsync(ruta);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            _ = RequestPermissions();
        }

        private async void OnNotificationActionTapped(NotificationActionEventArgs e)
        {
            if (e.IsTapped)
            {
                int encontrada = ManejoDeDatos.notaViewModel.EncontrarNota(e.Request.ReturningData);
                if (encontrada == -1)
                {
                    return;
                }

                var verNotasPage = new VerNotas(true, encontrada);

                await AppShell.Current.Navigation.PushAsync(verNotasPage); // Abre la notificacion pulsada
                return;
            }

        }
    }
}
