using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AutorCreacionDto, Autor>();
            CreateMap<Autor, AutorDto>();

            CreateMap <LibroCreacionDto, Libro>();
            CreateMap<Libro, LibroDto>();
        }
    }
}
