﻿using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if (produtos is null)
            return NotFound();

        //var destino = _mapper.Map<Destino>(origem);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();//400

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDto);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> UpdateParcial(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdReq)
    {
        if(patchProdReq is null || id <= 0){
            return BadRequest();
        }

        var produto = _uof.ProdutoRepository.Get( prod => prod.ProdutoId == id);
        if (produto is null)
            return NotFound();
        
        var updateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdReq.ApplyTo(updateRequest, ModelState);

        if(!ModelState.IsValid || TryValidateModel(updateRequest))
            return BadRequest(ModelState);

        _mapper.Map(updateRequest,produto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));


    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters prodParameters)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPaged(prodParameters);
        
        //var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        //return Ok(produtosDto);
        var metaData = new {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metaData));
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet("filter/preco/pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco filtroParams)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosFiltroPreco(filtroParams);
        var metaData = new {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };
        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metaData));
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("filter/nome/pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFilterNome([FromQuery] ProdutosFiltroNome filtroParams)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosFiltroNome(filtroParams);
        var metaData = new {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };
        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metaData));
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

}