using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public UsuariosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UsuarioDTO>> Get()
    {
        var usuarios = _uof.UsuarioRepository.GetAll();
        if (usuarios is null)
        {
            return NotFound();
        }
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        return Ok(usuariosDto);
    }

    [HttpGet("{id}", Name = "ObterUsuario")]
    public ActionResult<UsuarioDTO> Get(int id)
    {
        var usuario = _uof.UsuarioRepository.Get(c => c.UsuarioID == id);
        if (usuario is null)
        {
            return NotFound("Produto não encontrado...");
        }
        var usrDto = _mapper.Map<UsuarioDTO>(usuario);
        return Ok(usrDto);
    }

    [Authorize]
    [HttpPost]
    public ActionResult<UsuarioDTO> Post(UsuarioDTO usrDTO)
    {
        if (usrDTO is null)
            return BadRequest();

        var usr = _mapper.Map<Usuario>(usrDTO);

        var novoUsr = _uof.UsuarioRepository.Create(usr);
        _uof.Commit();

        var novoUsrDto = _mapper.Map<UsuarioDTO>(novoUsr);

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = novoUsrDto.UsuarioID }, novoUsrDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioDTO> Put(int id, UsuarioDTO usrDto)
    {
        if (id != usrDto.UsuarioID)
            return BadRequest();//400

        var usuario = _mapper.Map<Usuario>(usrDto);

        var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
        _uof.Commit();

        var usuarioAtualizadoDto = _mapper.Map<UsuarioDTO>(usuarioAtualizado);

        return Ok(usuarioAtualizadoDto);
    }

     [HttpDelete("{id:int}")]
    public ActionResult<UsuarioDTO> Delete(int id)
    {
        var usuario = _uof.UsuarioRepository.Get(p => p.UsuarioID == id);
        if (usuario is null)
        {
            return NotFound("Usuario não encontrado...");
        }

        var usuarioDeletado = _uof.UsuarioRepository.Delete(usuario);
        _uof.Commit();

        var usuarioDeletadoDto = _mapper.Map<UsuarioDTO>(usuarioDeletado);

        return Ok(usuarioDeletadoDto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<UsuarioDTO>> Get([FromQuery] UsuariosParameters usuariosParams)
    {
        var usuarios = _uof.UsuarioRepository.GetUsuariosPaged(usuariosParams);
        
        var metaData = new {
            usuarios.TotalCount,
            usuarios.PageSize,
            usuarios.CurrentPage,
            usuarios.TotalPages,
            usuarios.HasNext,
            usuarios.HasPrevious
        };

        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metaData));
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

        return Ok(usuariosDto);
    }

    [HttpGet("filter/nome/pagination")]
    public ActionResult<IEnumerable<UsuarioDTO>> GetUsuariosFilterNome([FromQuery] UsuariosFiltroNome filtroParams)
    {
        var usuarios = _uof.UsuarioRepository.GetUsuarioFiltroNome(filtroParams);
        var metaData = new {
            usuarios.TotalCount,
            usuarios.PageSize,
            usuarios.CurrentPage,
            usuarios.TotalPages,
            usuarios.HasNext,
            usuarios.HasPrevious
        };
        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metaData));
        var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        return Ok(usuariosDto);
    }


}