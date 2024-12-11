using RegistroEstudiante.App.Vistas;

namespace RegistroEstudiante.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListarEstudiante());
        }
    }
}

