using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonaAPI.Models
{
    public class Persona
    {
        [Key]
        [Column("id_persona")]
        public int? IdPersona { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("fecha_nacimiento")] 
        public DateTime FechaNacimiento { get; set; }   

        [Column("email")]
        public string Email { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
