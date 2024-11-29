using NotBook_Notes.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NotBook_Notes.Views;

public partial class Papelera : ContentPage
{
	public Papelera()
	{
		InitializeComponent();
		BorrarAntiguas();
		BindingContext = ManejoDeDatos.papeleraViewModel;
	}

	public void BorrarAntiguas()
	{
		while (ManejoDeDatos.papeleraViewModel.notas.Count() > 10)
		{
			ManejoDeDatos.papeleraViewModel.notas.Remove(ManejoDeDatos.papeleraViewModel.notas[0]);
		}
        string ruta = Path.Combine(ManejoDeDatos.GetRutaBackups(), "backup.json");
        ManejoDeDatos.GuardarDatosJSONAsync(ruta);
    }
}
