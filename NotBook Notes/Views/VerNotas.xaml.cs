using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Maui.NullableDateTimePicker;
using NotBook_Notes.Models;
using NotBook_Notes.ViewModels;
using System;

namespace NotBook_Notes.Views;

public partial class VerNotas : ContentPage
{
	bool esRecordatorio;
    bool esEdicion;
	DateTime? fechaLimite;
    int categoriaObjetivo;
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
            LabelFechaCreacion.Text = "Creación: " + DateTime.Now.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
            if (esRecordatorio)
            {
                this.Title = "Crear Recordatorio";
                FrameRecordatorio.IsVisible = true;
            }
        }
        else
        {
            Nota seleccionada = ColeccionNotas.notas[aEditar];
            TituloEditor.Text = seleccionada.Titulo;
            LabelFechaCreacion.Text = "Edición: " + seleccionada.FechaCreacion.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
            TxtNota.Text = seleccionada.Contenido;
            CategoriaPicker.SelectedIndex = seleccionada.Categoria;

            if (esRecordatorio)
            {
                Recordatorio Rseleccionada = ColeccionNotas.notas[aEditar] as Recordatorio;
                if (Rseleccionada == null)
                {
                    DisplayAlert("Error Fatal", "Ha ocurrido un error, intentalo de nuevo", "Volver");
                    Navigation.PopAsync();
                    return;
                }
                fechaLimite = Rseleccionada.fechaLimite;
                DateTimeEntry.Text = fechaLimite.Value.ToString("dddd, dd 'de' MMM yyyy hh:mm tt");
                this.Title = "Editar Recordatorio";
                FrameRecordatorio.IsVisible = true;
            }
            else
            {
                this.Title = "Editar Nota";
            }
        }

	}

    private async void BtnGuardarNota_Clicked(object sender, EventArgs e)
    {
        string titulo = TituloEditor.Text;
        //Minimo un titulo
        //if (string.IsNullOrWhiteSpace(TituloEditor.Text) || ColeccionNotas.notas.Find(u => u.Titulo == TituloEditor.Text) != null)
        //{
        //    IToast mensaje = Toast.Make("Introduzca un titulo valido");
        //    await mensaje.Show();
        //    return;
        //}

        //Si se esta editando, darle la nueva nota a la referencia que se obtuvo, si no, crearla nueva
        if (esEdicion)
        {
            if (!esRecordatorio)
            {
                Nota nuevaNota = new Nota(TituloEditor.Text, TxtNota.Text, DateTime.Now, categoriaObjetivo);
                ColeccionNotas.notas[aEditar] = nuevaNota;
            }
            else if (fechaLimite.HasValue)
            {
                Recordatorio nuevoRecordatorio = new Recordatorio(TituloEditor.Text, TxtNota.Text, DateTime.Now, fechaLimite.Value, categoriaObjetivo);
                ColeccionNotas.notas[aEditar] = nuevoRecordatorio;
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
                ColeccionNotas.notas.Add(nuevaNota);
                await Navigation.PopAsync();
            }
            else if (fechaLimite.HasValue)
            {
                //Creamos un recordatorio
                Recordatorio nuevoRecordatorio = new Recordatorio(TituloEditor.Text, TxtNota.Text, DateTime.Now, fechaLimite.Value, categoriaObjetivo);
                ColeccionNotas.notas.Add(nuevoRecordatorio);
                //llamamos a establecer la notificion correspondiente
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

    private void CargarCategorias()
    {
        foreach (var categoria in ManejoDeDatos.categorias)
        {
            CategoriaPicker.Items.Add(categoria.NombreCategoría);
        }

        CategoriaPicker.SelectedIndex = 0;
        ColorBoxView.BackgroundColor = ManejoDeDatos.categorias[0].ColorNotas;
    }

    private void OnColorBoxTapped(object sender, EventArgs e)
    {
        CategoriaPicker.Focus(); // Esto activará el Picker
    }

    private void OnCategoriaPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = CategoriaPicker.SelectedIndex;

        if (selectedIndex != -1)
        {
            // Obtener la categoría seleccionada
            string categoriaSeleccionada = CategoriaPicker.SelectedItem.ToString();

            // Encontrar el color correspondiente a la categoría seleccionada
            Color colorSeleccionado = ManejoDeDatos.categorias[selectedIndex].ColorNotas;

            // Cambiar el color del BoxView al de la categoría seleccionada
            ColorBoxView.BackgroundColor = colorSeleccionado;

            categoriaObjetivo = selectedIndex;
        }
    }


}