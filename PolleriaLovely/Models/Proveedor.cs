using PolleriaLovely.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolleriaLovely;

public class Proveedor
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProveedor { get; set; }
    [Required, StringLength(100)]
    public string NombreProveedor { get; set; }
    [Required, StringLength(11)]
    public string RucProveedor { get; set; }
    [StringLength(9)]
    public string Telefono { get; set; }
    [Required,StringLength(200)]
    public string Direccion { get; set; }

    public bool Estado { get; set; }

    public ICollection<Insumo> Insumos { get; set; }

}
