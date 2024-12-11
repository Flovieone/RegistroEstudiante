using RegistroEstudiante.AppMovil.Modelos;
using Firebase.Database;
using Firebase.Database.Query;

namespace RegistroEstudiante.AppMovil.Vistas;

public partial class CrearAlumno : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://registrosdeestudiantes-default-rtdb.firebaseio.com/");

    public List<Curso> Cursos { get; set; }

    public CrearAlumno()
    {
        InitializeComponent();
        ListarCursos();
        BindingContext = this;
    }

    private void ListarCursos()
    {
        var cursos = client.Child("Cursos").OnceAsync<Curso>();
        Cursos = cursos.Result.Select(x => x.Object).ToList();
    }

    private async void guardarButton_Clicked(object sender, EventArgs e)
    {
        Curso curso = cursoPicker.SelectedItem as Curso;

        var alumno = new Alumno
        {
            Nombres = primerNombresEntry.Text,
            PrimerApellido = primerApellidoEntry.Text,
            SegundoApellido = segundoApellidoEntry.Text,
            CorreoElectronico = correoEntry.Text,
            Edad = edadEntry.Text,
            Curso = curso
        };

        try
        {
            await client.Child("Alumnos").PostAsync(alumno);
            await DisplayAlert("Éxito", $"El Alumno {alumno.Nombres} {alumno.PrimerApellido} fue guardado correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
