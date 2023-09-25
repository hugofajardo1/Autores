using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDto, Autor>();
            CreateMap<Autor, AutorDto>();

            CreateMap<LibroCreacionDto, Libro>()
                .ForMember(libro => libro.AutorLibros, opciones => opciones.MapFrom(MapAutoresLibros));

            CreateMap<Libro, LibroDto>();

            CreateMap<ComentarioCreacionDto, Comentario>();
            CreateMap<Comentario, ComentarioDto>();
        }

        private List<AutorLibro> MapAutoresLibros(LibroCreacionDto libroCreacionDto, Libro libro)
        {
            var resultado = new List<AutorLibro>();
            if(libroCreacionDto.AutoresIds == null) { return resultado; }

            foreach (var autorId in libroCreacionDto.AutoresIds)
            {
                resultado.Add( new AutorLibro() { AutorId = autorId });
            }
            return resultado;
        }
    }
}
