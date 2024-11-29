using Newtonsoft.Json;
using NotBook_Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Android.OS;

namespace NotBook_Notes.Models
{
    public static class ManejoDeDatos
    {
        public static List<Categoria> categorias;
        public static NotaViewModel notaViewModel = new NotaViewModel();
        public static List<string> frasesBonitas = new List<string>();
        public static string nombreUsuario;


        //Puede usar la ruta default o una ruta donde se haya guardado un respaldo de las notas y configuraciones
        public static bool CargarDatosJSON(string ruta)
        {
            try
            {

                if (!File.Exists(ruta))
                {
                    Console.WriteLine("No se encontro el archivo");
                    return false;
                }

                string json = File.ReadAllText(ruta);
                var datos = JsonConvert.DeserializeObject<PlantillaDatosJson>(json);

                categorias.Clear();
                frasesBonitas.Clear();
                notaViewModel.notas.Clear();

                //Observable Collecion no cuenta con un AddRange, por desgracia D;
                foreach (Nota nota in datos.notasModelo)
                {
                    notaViewModel.notas.Add(nota);
                }
                foreach (Recordatorio record in datos.recordatoriosModelo)
                {
                    notaViewModel.notas.Add(record);
                }

                categorias.AddRange(datos.categoriasModelo);
                frasesBonitas.AddRange(datos.frasesBonitasModelo);
                nombreUsuario = datos.nombreUsuarioModelo;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        //Este usa la ruta default o la ruta docuemts para hacer un backup
        public static void GuardarDatosJSON(string Ruta)
        {
            try
            {
                List<Nota> notasAGuardar = new List<Nota>();
                List<Recordatorio> recordsAGuardar = new List<Recordatorio>();

                foreach (Nota nota in notaViewModel.notas)
                {
                    if (nota is Recordatorio record)
                    {
                        recordsAGuardar.Add(record);
                    }
                    else
                    {
                        notasAGuardar.Add(nota);
                    }
                }

                var datos = new PlantillaDatosJson
                {
                    notasModelo = notasAGuardar,
                    recordatoriosModelo = recordsAGuardar,
                    categoriasModelo = categorias,
                    frasesBonitasModelo = frasesBonitas,
                    nombreUsuarioModelo = nombreUsuario
                };

                string json = JsonConvert.SerializeObject(datos, Formatting.Indented);
                File.WriteAllText(Ruta, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static string GetRutaBackups()
        {
            var folderPath = Path.Combine(FileSystem.AppDataDirectory, "Backup");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public static string GetRutaDocumentos()
        {
            // Obtener la ruta a la carpeta Documents
            var path = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "BackupNotas");

            // Crear la carpeta si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }

    //Aqui puedo dejar a la clase plantilla para recibir de los json
    public class PlantillaDatosJson
    {
        public List<Nota> notasModelo;
        public List<Recordatorio> recordatoriosModelo;
        public List<Categoria> categoriasModelo;
        public List<string> frasesBonitasModelo;
        public string nombreUsuarioModelo;
    }
}
