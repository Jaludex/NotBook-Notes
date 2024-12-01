using Android.Content;
using Android.OS;
using Android.Provider;
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

                bool respuesta = await Application.Current.MainPage.DisplayAlert("Una Ultima Cosa...", $"{ManejoDeDatos.nombreUsuario}, en los dispositivos de hoy en dia, las optimizaciones del sistema pueden acabar con nuestro preciado servicio de notificaciones.{System.Environment.NewLine}Para que no sufras ese problema, puedes ir a la opciones de la app y buscar un menu del estilo 'Ahorro de Bateria' y desactivarlo, o seleccionar 'Sin reestricciones'.{System.Environment.NewLine}{System.Environment.NewLine}¿Quieres ir a las configuraciones de la app ahora?", "Configuraciones", "No Importa");
                if (respuesta)
                {
                    var intent = new Intent();
                    intent.SetAction(Settings.ActionRequestIgnoreBatteryOptimizations);
                    intent.SetData(Android.Net.Uri.FromParts("package", Android.App.Application.Context.PackageName, null));
                    intent.AddFlags(ActivityFlags.NewTask);

                    // Verifica si se encontro la ventana de optimizar bateria
                    if (intent.ResolveActivity(Android.App.Application.Context.PackageManager) != null)
                    {
                        Android.App.Application.Context.StartActivity(intent);
                    }
                    //Si no, abre las opciones comunes de la app
                    else
                    {
                        intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                        intent.AddFlags(ActivityFlags.NewTask);
                        intent.SetData(Android.Net.Uri.FromParts("package", Android.App.Application.Context.PackageName, null));
                        Android.App.Application.Context.StartActivity(intent);
                    }
                }

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
            int encontrada = ManejoDeDatos.notaViewModel.EncontrarNota(e.Request.ReturningData);
            if (encontrada == -1)
            {
                return;
            }
            if (e.IsTapped)
            {

                var verNotasPage = new VerNotas(true, encontrada);

                await AppShell.Current.Navigation.PushAsync(verNotasPage); // Abre la notificacion pulsada
                return;
            }
            if (e.IsDismissed)
            {
                ManejoDeDatos.notaViewModel.notas.Remove(ManejoDeDatos.notaViewModel.notas[encontrada]);
            }

        }
    }
}
