using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string Name { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }
    }
}
    