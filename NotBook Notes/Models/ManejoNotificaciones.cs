using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotBook_Notes.Models
{
    internal static class ManejoNotificaciones
    {
        private static int cantidadNotificaciones;
        public static async void CrearNotificacion(Recordatorio recordatorio)
        {
            cantidadNotificaciones++;
            var notification = new NotificationRequest
            {
                NotificationId = cantidadNotificaciones,
                Title = recordatorio.Titulo,
                Description = $"¡Oye {ManejoDeDatos.nombreUsuario}! {Environment.NewLine}Recuerda: {recordatorio.Contenido}",
                ReturningData = recordatorio.Titulo,
                Schedule =
                {
                     NotifyTime = recordatorio.fechaLimite
                }
            };
            await LocalNotificationCenter.Current.Show(notification);
        }

        public static async void BorrarNotificacion(Recordatorio recordABorrar)
        {
            var IlistnotisPendientes = LocalNotificationCenter.Current.GetPendingNotificationList().Result;
            var notisPendientes = IlistnotisPendientes.ToList();
            foreach (var noti in notisPendientes)
            {
                if (noti.Title == recordABorrar.Titulo)
                {
                    LocalNotificationCenter.Current.Cancel(noti.NotificationId);
                    return;
                }
            }
        }
    }
}
