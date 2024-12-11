using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Controls;
using RegistroEstudiante.App.Modelos;

namespace RegistroEstudiante.App.Vistas
{
    public partial class CrearEstudiante : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://estudiantes-10217-default-rtdb.firebaseio.com/");
        public event EventHandler EstudianteGuardado;

        public CrearEstudiante()
        {
            InitializeComponent();
            CargarCursos();
        }

        private async void CargarCursos()
        {
            var cursos = await firebaseClient.Child("Cursos").OnceAsync<Curso>();
            List<string> nombresCursos = new List<string>();

            foreach (var curso in cursos)
            {
                nombresCursos.Add(curso.Object.Nombre);
            }

            cursoPicker.ItemsSource = nombresCursos;

            if (nombresCursos.Count == 0)
            {
                await DisplayAlert("Error", "No se encontraron cursos en la base de datos.", "OK");
            }
        }

        private async void guardarButton_Clicked(object sender, EventArgs e)
        {
            var cursoSeleccionado = cursoPicker.SelectedItem?.ToString();
            var nombres = nombresEntry.Text;
            var primerApellido = primerApellidoEntry.Text;
            var segundoApellido = segundoApellidoEntry.Text;
            var correo = correoEntry.Text;
            var edad = edadEntry.Text;

            if (string.IsNullOrWhiteSpace(cursoSeleccionado) ||
                string.IsNullOrWhiteSpace(nombres) ||
                string.IsNullOrWhiteSpace(primerApellido) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(edad))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var estudiante = new Estudiante
            {
                Nombres = nombres,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                CorreoElectronico = correo,
                Edad = int.Parse(edad),
                Curso = cursoSeleccionado,
                Activo = true // Por defecto, todos los estudiantes están activos al crearse
            };

            await firebaseClient.Child("Cursos").Child(estudiante.Curso).Child("Estudiantes").PostAsync(estudiante);

            await DisplayAlert("Éxito", "Estudiante guardado correctamente.", "OK");
            EstudianteGuardado?.Invoke(this, EventArgs.Empty); // Invocar el evento para actualizar la lista
            await Navigation.PopToRootAsync();
        }
    }
}
