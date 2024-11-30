using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Maui.NullableDateTimePicker;
using Microsoft.Maui.Controls.Platform;
using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using System;

namespace NotBook_Notes.Views;

public partial class VerNotas : ContentPage
{
	bool esRecordatorio;
    bool esEdicion;
	DateTime? fechaLimite;
    Categoria categoriaObjetivo;
    int aEditar;

    public VerNotas(bool esRecordatorio)
	{
		InitializeComponent();
        this.esRecordatorio = esRecordatorio;
        esEdicion = false;
        CargarCategorias();
        CambiarContexto();
	}

    //Este es para el caso de edicion/visualizacion, recibe el indice de la nota que se abre, que en caso de editarla, lo hara a traves de ese indice
    public VerNotas(bool esRecordatorio, int aEditar)
    {
        InitializeComponent();
        this.aEditar = aEditar;
        this.esRecordatorio = esRecordatorio;
        esEdicion = true;
        CargarCategorias();
        CambiarContexto();
    }

    private void CambiarContexto()
	{
        fechaLimite = null;

        //Aqui coloco el titulo que va acorde al contexto, el default ya es crear nota asi que ese caso me lo salto
        if (!esEdicion)
        {
                    CategoriaPicker.SelectedIndex = 0;
        ColorBoxView.BackgroundColor = ManejoDeDatos.categorias[0].ColorNotas;
            LabelFechaCreacion.Text = "Creación: " + DateTime.Now.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
            if (esRecordatorio)
            {
                this.Title = "Crear Recordatorio";
                FrameRecordatorio.IsVisible = true;
            }
        }
        else
        {
            Nota seleccionada = ManejoDeDatos.notaViewModel.notas[aEditar];
            TituloEditor.Text = seleccionada.Titulo;
            LabelFechaCreacion.Text = "Edición: " + seleccionada.FechaCreacion.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
            TxtNota.Text = seleccionada.Contenido;
            Console.WriteLine(seleccionada.Categoria.NombreCategoria + "sexo");
            CategoriaPicker.SelectedItem = seleccionada.Categoria.NombreCategoria;
            categoriaObjetivo = seleccionada.Categoria;
            ColorBoxView.BackgroundColor = seleccionada.Categoria.ColorNotas;
            btnBorrar.IsVisible = true;

            if (esRecordatorio)
            {
                Recordatorio Rseleccionada = ManejoDeDatos.notaViewModel.notas[aEditar] as Recordatorio;
                if (Rseleccionada == null)
                {
                    DisplayAlert("Error Fatal", "Ha ocurrido un error, intentalo de nuevo", "Volver");
                    Navigation.PopAsync();
                    return;
                }
                fechaLimite = Rseleccionada.fechaLimite;
                DateTimeEntry.Text = fechaLimite.Value.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
                this.Title = "Ver/Editar Recordatorio";
                FrameRecordatorio.IsVisible = true;
            }
            else
            {
                this.Title = "Ver/Editar Nota";
            }
        }

	}

    private async void BtnGuardarNota_Clicked(object sender, EventArgs e)
    {
        try
        {
            string titulo = TituloEditor.Text;
            //Minimo un titulo
            if (string.IsNullOrWhiteSpace(titulo))
            {
                IToast mensaje = Toast.Make("Introduzca un titulo valido");
                await mensaje.Show();
                return;
            }

            if (TxtNota.Text == null)
            {
                TxtNota.Text = "";
            }

            //Si se esta editando, darle la nueva nota a la referencia que se obtuvo, si no, crearla nueva
            if (esEdicion)
            {
                if (!esRecordatorio)
                {
                    Nota nuevaNota = new Nota(TituloEditor.Text, TxtNota.Text, ManejoDeDatos.notaViewModel.notas[aEditar].FechaCreacion, categoriaObjetivo);
                    ManejoDeDatos.notaViewModel.notas[aEditar] = nuevaNota;
                    ManejoDeDatos.notaViewModel.notas[aEditar].FechaCreacion = DateTime.Now;
                    //Cambio esto al momento para asegurar que se llame al property changed

                    IToast mensaje = Toast.Make("Nota Guardada");
                    await mensaje.Show();
                }
                else if (fechaLimite.HasValue)
                {
                    Recordatorio nuevoRecordatorio = new Recordatorio(TituloEditor.Text, TxtNota.Text, ManejoDeDatos.notaViewModel.notas[aEditar].FechaCreacion, fechaLimite.Value, categoriaObjetivo);
                    ManejoDeDatos.notaViewModel.notas[aEditar] = nuevoRecordatorio;
                    ManejoDeDatos.notaViewModel.notas[aEditar].FechaCreacion = DateTime.Now;

                    //Aqui llamamos a quitar la notificacion anterior y colocar la que tiene la nueva fecha limite
                    IToast mensaje = Toast.Make("Recordatorio Guardado");
                    await mensaje.Show();
                }
                else
                {
                    IToast mensaje = Toast.Make("Introduzca una Fecha Valida");
                    await mensaje.Show();
                    return;
                }
            }
            //Es de creacion
            else
            {
                if (!esRecordatorio)
                {
                    //Creamos una nota comun
                    Nota nuevaNota = new Nota(TituloEditor.Text, TxtNota.Text, DateTime.Now, categoriaObjetivo);
                    ManejoDeDatos.notaViewModel.AddNota(nuevaNota);
                    IToast mensaje = Toast.Make("Nota Creada");
                    await mensaje.Show();
                    await Navigation.PopAsync();
                }
                else if (fechaLimite.HasValue)
                {
                    //Creamos un recordatorio
                    Recordatorio nuevoRecordatorio = new Recordatorio(TituloEditor.Text, TxtNota.Text, DateTime.Now, fechaLimite.Value, categoriaObjetivo);
                    ManejoDeDatos.notaViewModel.AddNota(nuevoRecordatorio);
                    //llamamos a establecer la notificion correspondiente
                    IToast mensaje = Toast.Make("Recordatorio Creado");
                    await mensaje.Show();
                    await Navigation.PopAsync();
                }
                else
                {
                    IToast mensaje = Toast.Make("Introduzca una Fecha Valida");
                    await mensaje.Show();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "Volver");
            await Navigation.PopAsync();
        }

    }

    private async void btnBorrar_Clicked(object sender, EventArgs e)
    {
        if (esEdicion)
        {
            bool answer = await DisplayAlert("¿Estas Seguro?", "¿Quieres Borrar este elemento?", "Borrar", "Cancelar");
            if (answer)
            {
                ManejoDeDatos.papeleraViewModel.notas.Add(ManejoDeDatos.notaViewModel.notas[aEditar]);
                ManejoDeDatos.notaViewModel.notas.Remove(ManejoDeDatos.notaViewModel.notas[aEditar]);
                string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
                await ManejoDeDatos.GuardarDatosJSONAsync(ruta);
                await Navigation.PopAsync();
            }
        }

    }

    //De aqui abajo, se maneja el date picker y el color picker

    private async void DateTimePicker_Clicked(object sender, EventArgs e)
	{
        INullableDateTimePickerOptions nullableDateTimePickerOptions = new NullableDateTimePickerOptions
        {
            NullableDateTime = fechaLimite,
            Mode = PickerModes.DateTime,
            ShowWeekNumbers = true,
            MinDate = DateTime.Now,
            CancelButtonText = "Cancelar",
            OkButtonText = "Elegir",
            ClearButtonText = "Borrar",
            Is12HourFormat = true,
        };

        var result = await NullableDateTimePicker.OpenCalendarAsync(nullableDateTimePickerOptions);
        if (result is PopupResult popupResult && popupResult.ButtonResult != PopupButtons.Cancel)
        {
            fechaLimite = popupResult.NullableDateTime;
            if (fechaLimite != null)
            {
                DateTimeEntry.Text = "Fecha Limite: " + popupResult.NullableDateTime?.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
            }
            else
            {
                DateTimeEntry.Text = "Fecha Limite: A Escoger";
            }
        }
    }

    //Que tocar el frame de la fecha tambien abra el calendario
    private void OnFrameRecordatorioTapped(object sender, EventArgs e)
    {
        DateTimePicker_Clicked(sender, e);
    }

    private void CargarCategorias()
    {
        foreach (var categoria in ManejoDeDatos.categorias)
        {
            CategoriaPicker.Items.Add(categoria.NombreCategoria);
        }
    }

    private void OnColorBoxTapped(object sender, EventArgs e)
    {
        CategoriaPicker.Focus(); // Esto activará el Picker cuando se le de al circulo de color
    }

    private void OnCategoriaPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = CategoriaPicker.SelectedIndex;

        if (selectedIndex != -1)
        {
            categoriaObjetivo = ManejoDeDatos.categorias[selectedIndex];

            // Cambiar el color del BoxView al de la categoría seleccionada
            ColorBoxView.BackgroundColor = categoriaObjetivo.ColorNotas;

        }
    }
}