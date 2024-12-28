using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEstudiante.Modelos;

public class FirebaseService
{
    private FirebaseClient client = new FirebaseClient("https://registrosdeestudiantes-default-rtdb.firebaseio.com/");

    public async Task<List<Alumno>> ObtenerAlumnos()
    {
        var alumnos = await client
            .Child("Alumnos")
            .OnceAsync<Alumno>();

        return alumnos.Select(a => a.Object).ToList();
    }

    public async Task AgregarAlumno(Alumno alumno)
    {
        await client
            .Child("Alumnos")
            .PostAsync(alumno);
    }

    public async Task ActualizarAlumno(Alumno alumno)
    {
        await client
            .Child("Alumnos")
            .Child(alumno.Id.ToString())
            .PutAsync(alumno);
    }

    public async Task DeshabilitarAlumno(string id)
    {
        var alumno = (await client
            .Child("Alumnos")
            .OnceAsync<Alumno>())
            .FirstOrDefault(a => a.Object.Id == id)?.Object;

        if (alumno != null)
        {
            alumno.IsActive = false;
            await client
                .Child("Alumnos")
                .Child(alumno.Id.ToString())
                .PutAsync(alumno);
        }
    }

    public async Task<List<Curso>> ObtenerCursos()
    {
        var cursos = await client
            .Child("Cursos")
            .OnceAsync<Curso>();

        return cursos.Select(c => c.Object).ToList();
    }
}
