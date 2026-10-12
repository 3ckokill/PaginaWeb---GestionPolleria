using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolleriaLovely.Models
{
    public class Rol
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }

        [Required, StringLength(50)]
        public string NombreRol { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public bool Estado { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
