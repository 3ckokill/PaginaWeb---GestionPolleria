using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolleriaLovely.Models
{
    public class Producto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        [Required, StringLength(100)]
        public string NombreProducto { get; set; }
        [Required,StringLength(200)]

        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }

        public string Estado { get; set; }

        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}
