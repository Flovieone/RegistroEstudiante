using RegistroEstudiante.Modelos;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace RegistroEstudiante.AppMovil.Vistas
{
    public partial class DetalleAlumno : ContentPage
    {
        FirebaseService firebaseService = new FirebaseService();
        public Alumno Alumno { get; set; }

        public DetalleAlumno(Alumno alumno)
        {
            InitializeComponent();
            Alumno = alumno;
            BindingContext = this;
        }

        private async void GuardarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await firebaseService.ActualizarAlumno(Alumno);
                await DisplayAlert("Éxito", "El alumno ha sido actualizado correctamente", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void EliminarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await firebaseService.DeshabilitarAlumno(Alumno.Id);
                await DisplayAlert("Éxito", "El alumno ha sido deshabilitado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
