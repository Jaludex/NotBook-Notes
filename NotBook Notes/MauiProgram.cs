﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotBook_Notes.Models;

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
            ManejoDeDatos.categorias = new List<Categoria>();


            return builder.Build();
        }
    }
}
