using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Logging;
using RegistroEstudiante.App.Modelos;

namespace RegistroEstudiante.App
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

            RegistrarCursos(); 

            return builder.Build();
        }

        public static async void RegistrarCursos()
        {
            FirebaseClient client = new("https://estudiantes-10217-default-rtdb.firebaseio.com/");

            var cursos = await client.Child("Cursos").OnceAsync<Curso>();

            if (!cursos.Any())
            {
                string[] nombresCursos = new string[]
                {
                    "4° Básico", "5° Básico", "6° Básico", "7° Básico", "8° Básico",
                    "1° Medio", "2° Medio", "3° Medio", "4° Medio"
                };

                foreach (var nombre in nombresCursos)
                {
                    await client.Child("Cursos").PostAsync(new Curso { Nombre = nombre });
                }
            }
        }
    }
}
