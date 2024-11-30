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
        public static async void CrearNotificacion(Recordatorio recordatorio)
        {
            ManejoDeDatos.cantidadNotificaciones++;
            var notification = new NotificationRequest
            {
                NotificationId = ManejoDeDatos.cantidadNotificaciones,
                Title = recordatorio.Titulo,
                Description = $"¡Oye {ManejoDeDatos.nombreUsuario}! {Environment.NewLine}" + (!string.IsNullOrEmpty(recordatorio.Contenido) ? "Recuerda: " + recordatorio.Contenido : "Ya se acabo el tiempo que estableciste, " + ManejoDeDatos.nombreUsuario),
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
            int encontrado = GetIDNotificacion(recordABorrar);
            if (encontrado != -1)
            {
                LocalNotificationCenter.Current.Cancel(encontrado);
            }
        }

        public static async void EditarNotificacion(Recordatorio recordAEditar, Recordatorio nuevoRecord)
        {
            int encontrado = GetIDNotificacion(recordAEditar);
            if (encontrado != -1)
            {
                LocalNotificationCenter.Current.Cancel(encontrado);
                var notification = new NotificationRequest
                {
                    NotificationId = encontrado,
                    Title = nuevoRecord.Titulo,
                    Description = $"¡Oye {ManejoDeDatos.nombreUsuario}! {Environment.NewLine}" + (!string.IsNullOrEmpty(nuevoRecord.Contenido) ? "Recuerda: " + nuevoRecord.Contenido : "Ya se acabo el tiempo que estableciste, " + ManejoDeDatos.nombreUsuario),
                    ReturningData = nuevoRecord.Titulo,
                    Schedule =
                    {
                         NotifyTime = nuevoRecord.fechaLimite
                    }
                };
                await LocalNotificationCenter.Current.Show(notification);
                return;
            }

            //En caso de que la notificacion ya haya pasado, en vez de editar solo la crea de nuevo
            CrearNotificacion(nuevoRecord);
        }

        private static int GetIDNotificacion(Recordatorio recordatorio)
        {
            var IlistnotisPendientes = LocalNotificationCenter.Current.GetPendingNotificationList().Result;
            var notisPendientes = IlistnotisPendientes.ToList();
            foreach (var noti in notisPendientes)
            {
                if (noti.Title == recordatorio.Titulo)
                {
                    return noti.NotificationId;
                }
            }

            return -1;
        }
    }
}
