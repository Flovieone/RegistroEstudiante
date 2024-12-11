using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Controls;
using RegistroEstudiante.App.Modelos;

namespace RegistroEstudiante.App.Vistas
{
    public partial class ListarEstudiante : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://estudiantes-10217-default-rtdb.firebaseio.com/");
        List<Estudiante> estudiantesList = new List<Estudiante>();

        public ListarEstudiante()
        {
            InitializeComponent();
            CargarEstudiantes();
        }

        private async void CargarEstudiantes()
        {
            try
            {
                var cursos = await firebaseClient.Child("Cursos").OnceAsync<Curso>();
                estudiantesList.Clear();

                foreach (var curso in cursos)
                {
                    var estudiantes = await firebaseClient.Child("Cursos").Child(curso.Object.Nombre).Child("Estudiantes").OnceAsync<Estudiante>();
                    estudiantesList.AddRange(estudiantes.Select(e => new Estudiante
                    {
                        Key = e.Key,
                        Nombres = e.Object.Nombres,
                        PrimerApellido = e.Object.PrimerApellido,
                        SegundoApellido = e.Object.SegundoApellido,
                        CorreoElectronico = e.Object.CorreoElectronico,
                        Curso = e.Object.Curso,
                        Edad = e.Object.Edad,
                        Activo = e.Object.Activo,
                        Imagen = e.Object.Imagen
                    }).ToList());
                }

                estudiantesCollectionView.ItemsSource = estudiantesList;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al cargar estudiantes: {ex.Message}", "OK");
            }
        }

        private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = e.NewTextValue.ToLower();
            var estudiantesFiltrados = estudiantesList.Where(est =>
                est.Nombres.ToLower().Contains(filtro) ||
                est.PrimerApellido.ToLower().Contains(filtro) ||
                est.SegundoApellido.ToLower().Contains(filtro)).ToList();

            estudiantesCollectionView.ItemsSource = estudiantesFiltrados;
        }

        private async void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            var switchControl = sender as Switch;
            var estudiante = switchControl.BindingContext as Estudiante;

            if (estudiante != null && !string.IsNullOrEmpty(estudiante.Key))
            {
                estudiante.Activo = e.Value;
                await firebaseClient.Child("Cursos").Child(estudiante.Curso).Child("Estudiantes").Child(estudiante.Key).PutAsync(estudiante);
            }
            else
            {
                await DisplayAlert("Error", "Error al actualizar el estado del estudiante. Clave del estudiante no encontrada.", "OK");
            }
        }

        private async void VerDetallesButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var estudiante = button.BindingContext as Estudiante;

            if (estudiante != null)
            {
                await Navigation.PushAsync(new DetallesEstudiante(estudiante));
            }
        }

        private async void NuevoEmpleadoBoton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearEstudiante());
        }
    }
}
