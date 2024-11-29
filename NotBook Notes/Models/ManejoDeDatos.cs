using Newtonsoft.Json;
using NotBook_Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                notaViewModel.notas = datos.notasModelo;
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

        public static void GuardarDatosJSON(string Ruta)
        {
            try
            {

            }
            catch
            {

            }

        }
    }

    //Aqui puedo dejar a la clase plantilla para recibir de los json
    public class PlantillaDatosJson
    {
        public ObservableCollection<Nota> notasModelo;
        public List<Categoria> categoriasModelo;
        public List<string> frasesBonitasModelo = new List<string>();
        public string nombreUsuarioModelo;
    }
}
