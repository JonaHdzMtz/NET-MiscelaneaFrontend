using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiscelaneaFrontend.Models
{
    public class ProductDTO
    {

        public long IdProduct { get; set; }
        [Required(ErrorMessage ="campo requerido")]
        [DisplayName("Nombre de producto")]
        public string ProductName { get; set; } = "";
        [DisplayName("Descripcion")]
        public string? ProductDescription { get; set; }
        [Required(ErrorMessage = "campo requerido")]
        [DisplayName("Precio")]
        public double Price { get; set; }
        [Required(ErrorMessage = "campo requerido")]
        [DisplayName("Cantidad")]
        public long? Stock { get; set; }
    }
}

