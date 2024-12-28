using RegistroEstudiante.Modelos;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEstudiante.AppMovil.Vistas
{
    public partial class CrearAlumno : ContentPage
    {
        FirebaseService firebaseService = new FirebaseService();

        public List<Curso> Cursos { get; set; }

        public CrearAlumno()
        {
            InitializeComponent();
            ListarCursos();
            BindingContext = this;
        }

        private async void ListarCursos()
        {
            Cursos = await firebaseService.ObtenerCursos();
            OnPropertyChanged(nameof(Cursos));
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
                await firebaseService.AgregarAlumno(alumno);
                await DisplayAlert("Éxito", $"El Alumno {alumno.NombreCompleto} fue guardado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
