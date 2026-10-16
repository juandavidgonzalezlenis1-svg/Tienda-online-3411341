using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.API.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? EmailContacto { get; set; }

        [MaxLength(30)]
        public string? Telefono { get; set; }
    }
}
