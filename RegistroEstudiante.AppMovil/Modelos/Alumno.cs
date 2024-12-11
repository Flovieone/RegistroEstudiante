using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEstudiante.AppMovil.Modelos
{
    public class Alumno
    {
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Edad { get; set; }
        public required Curso Curso { get; set; }
        public string NombreCompleto => $"{Nombres} {PrimerApellido} {SegundoApellido}";
    }
}
