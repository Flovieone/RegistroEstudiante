namespace RegistroEstudiante.Modelos
{
    public class Alumno
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Edad { get; set; }
        public Curso Curso { get; set; }
        public bool IsActive { get; set; } = true;

        public string NombreCompleto => $"{Nombres} {PrimerApellido} {SegundoApellido}";
    }
}
