using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [StringLength(200)]

        public string? Descripcion { get; set; }
        [Range(typeof(decimal), "0.01", "1000000", ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        public bool Estado { get; set; }
        [Required]
        public int IdCategoria { get; set; }
        [ValidateNever]
        public Categoria Categoria { get; set; }
    }
}

