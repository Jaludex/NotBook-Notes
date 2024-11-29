using CommunityToolkit.Maui;
using Maui.NullableDateTimePicker;
using Microsoft.Extensions.Logging;
using NotBook_Notes.Models;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace NotBook_Notes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                .ConfigureNullableDateTimePicker()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Rubik-Light.ttf", "RubikLight");
                    fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
                    fonts.AddFont("Rubik-Medium.ttf", "RubikMedium");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Tamporal, solo para crear algo donde trabajar
            //ManejoDeDatos.notas = new List<Nota>();
            // Inicializa la lista de categorías
            ManejoDeDatos.categorias = new List<Categoria>();

            // Agregar categoría "Personal" con color amarillo
            ManejoDeDatos.categorias.Add(new Categoria("Personal", Color.FromArgb("#FFFFD700"))); // Amarillo (LightGoldenrodYellow)

            // Agregar otras categorías
            ManejoDeDatos.categorias.Add(new Categoria("Trabajo", Color.FromArgb("#FF87CEEB"))); // Azul (LightSkyBlue)
            ManejoDeDatos.categorias.Add(new Categoria("Estudios", Color.FromArgb("#FF90EE90"))); // Verde (LightGreen)
            ManejoDeDatos.categorias.Add(new Categoria("Ideas", Color.FromArgb("#FFF08080"))); // Coral (LightCoral)
            ManejoDeDatos.categorias.Add(new Categoria("Compras", Color.FromArgb("#FFFFB6C1"))); // Rosa claro (LightPink)
            ManejoDeDatos.categorias.Add(new Categoria("Bienestar", Color.FromArgb("#FF20B2AA"))); // Verde mar (LightSeaGreen)
            ManejoDeDatos.categorias.Add(new Categoria("Eventos", Color.FromArgb("#FFD3D3D3"))); // Gris claro (LightGray)
            ManejoDeDatos.categorias.Add(new Categoria("Viajes", Color.FromArgb("#FFB0E0E6"))); // Azul acero (LightSteelBlue)
            ManejoDeDatos.categorias.Add(new Categoria("Hobbies", Color.FromArgb("#FFFFA500")));

            ManejoDeDatos.CargarDatosJSON(Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json"));
            return builder.Build();
        }
    }
}
