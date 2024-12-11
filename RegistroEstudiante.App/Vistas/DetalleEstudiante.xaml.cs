using Microsoft.Maui.Controls;
using RegistroEstudiante.App.Modelos;

namespace RegistroEstudiante.App.Vistas
{
    public partial class DetallesEstudiante : ContentPage
    {
        public DetallesEstudiante(Estudiante estudiante)
        {
            InitializeComponent();
            BindingContext = estudiante;
        }
    }
}
