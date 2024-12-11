namespace RegistroEstudiante.App.Modelos
{
    public class Estudiante
    {
        public string Key { get; set; } // Esta propiedad almacenará la clave generada por Firebase
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Curso { get; set; }
        public int Edad { get; set; }
        public bool Activo { get; set; } = true;
        public string Imagen { get; set; } // Añadir la propiedad para la imagen
    }
}
