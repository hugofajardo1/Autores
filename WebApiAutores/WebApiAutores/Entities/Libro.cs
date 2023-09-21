using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string Title { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<AutorLibro> AutorLibros { get; set;}
    }
}
