using System.Collections.ObjectModel;
using RegistroEstudiante.Modelos;
using System.Linq;

namespace RegistroEstudiante.AppMovil.Vistas;

public partial class ListarAlumnos : ContentPage
{
    FirebaseService firebaseService = new FirebaseService();
    public ObservableCollection<Alumno> Lista { get; set; } = new ObservableCollection<Alumno>();

    public ListarAlumnos()
    {
        InitializeComponent();
        BindingContext = this;
        CargarLista();
    }

    private async void CargarLista()
    {
        var alumnos = await firebaseService.ObtenerAlumnos();
        foreach (var alumno in alumnos)
        {
            Lista.Add(alumno);
        }
    }

    private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroSearchBar.Text.ToLower();

        if (!string.IsNullOrEmpty(filtro))
        {
            listaCollection.ItemsSource = Lista.Where(x => x.NombreCompleto.ToLower().Contains(filtro)).ToList();
        }
        else
        {
            listaCollection.ItemsSource = Lista;
        }
    }

    private async void NuevoAlumnoBoton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearAlumno());
    }

    private async void DetallesButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var alumno = button.BindingContext as Alumno;

        if (alumno != null)
        {
            await Navigation.PushAsync(new DetalleAlumno(alumno));
        }
    }
}
