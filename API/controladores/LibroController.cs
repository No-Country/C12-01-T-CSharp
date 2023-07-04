using AutoMapper;
using Core.Entities;
using Infrastructure.repositorios;
using Microsoft.AspNetCore.Mvc;

namespace API.controladores
{
    [ApiController]
    [Route("api/[Controlador]")]
    public class LibroController : Controller
    {
        private readonly GenericoRepo<Autor> _autorRepo;
        private readonly GenericoRepo<Genero> _generoRepo;
        private readonly GenericoRepo<Libro> _libroRepo;
        private readonly IMapper _mapper;

        public LibroController(GenericoRepo<Autor> autorRepo, GenericoRepo<Genero> generoRepo, GenericoRepo<Libro> libroRepo, IMapper mapper)
        {
            _autorRepo = autorRepo;
            _generoRepo = generoRepo;
            _libroRepo = libroRepo;
            _mapper = mapper;
        }
    }
}



