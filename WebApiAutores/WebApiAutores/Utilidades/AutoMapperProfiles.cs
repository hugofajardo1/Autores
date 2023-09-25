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

            CreateMap<Libro, LibroDto>()
                .ForMember(libroDto => libroDto.Autores, opciones => opciones.MapFrom(MapLibroDtoAutores))
                .ForMember(libroDto => libroDto.Comentarios, opciones => opciones.MapFrom(MapLibroDtoComentarios));

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

        private List<AutorDto> MapLibroDtoAutores(Libro libro,  LibroDto libroDto)
        {
            var resultado = new List<AutorDto>();
            if (libro.AutorLibros==null) { return resultado; }

            foreach (var autorLibro in libro.AutorLibros)
            {
                resultado.Add(new AutorDto()
                {
                    Id = autorLibro.AutorId,
                    Name = autorLibro.Autor.Name
                });
            }
            return resultado;
        }

        private List<ComentarioDto> MapLibroDtoComentarios(Libro libro, LibroDto libroDto)
        {
            var resultado = new List<ComentarioDto>();
            if (libro.Comentarios==null) { return resultado; }

            foreach (var comentario in libro.Comentarios)
            {
                resultado.Add(new ComentarioDto()
                { 
                    Id = comentario.Id, Content = comentario.Content 
                });
            }
            return resultado;
        }
    }
}
