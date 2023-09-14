namespace WebApiAutores.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
    }
}
