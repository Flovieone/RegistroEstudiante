using RegistroEstudiante.AppMovil.Modelos;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;

namespace RegistroEstudiante.AppMovil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            Registrar();
            return builder.Build();
        }

        public static void Registrar()
        {
            FirebaseClient client = new FirebaseClient("https://registrosdeestudiantes-default-rtdb.firebaseio.com/");

            var cursos = client.Child("Cursos").OnceAsync<Curso>();

            if (cursos.Result.Count == 0)
            {
                client.Child("Cursos").PostAsync(new Curso { Nombre = "3° Medio" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "2° Medio" });
                client.Child("Cursos").PostAsync(new Curso { Nombre = "1° Medio" });
            }
        }
    }
}
