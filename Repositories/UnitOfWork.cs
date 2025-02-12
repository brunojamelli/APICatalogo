﻿using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;

    private IUsuarioRepository? _usuarioRepo;

    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            //if (_produtoRepo == null)
            //{
            //    _produtoRepo = new ProdutoRepository(_context);
            //}
            //return _produtoRepo;
        }
    }
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public IUsuarioRepository UsuarioRepository
    {
        get
        {
            return _usuarioRepo = _usuarioRepo ?? new UsuarioRepository(_context);
        }
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
