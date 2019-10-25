using System;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic; 
using Newtonsoft.Json;
using backend.ViewModels;

namespace BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase  {

        VallerContext _context = new VallerContext();

        // GET: api/Categoria/
        [HttpGet]
        public async Task<ActionResult<List<Oferta>>> Get()
        {
            var oferta = await _context.Oferta.Include(p => p.IdProdutoNavigation).ToListAsync();

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> Get(int id)
        {
            var oferta = await _context.Oferta.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

         [HttpPost]
        public async Task<ActionResult<Oferta>> Post(Oferta oferta)
        {
            try
            {
                await _context.AddAsync(oferta);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return oferta;
        }
         [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, OfertaViewModel oferta)
        {
            if (id != oferta.IdOferta)
            {
                return BadRequest();
            }

            Oferta ofertaBanco = _context.Oferta.Find(id);
            
            if (oferta.IdProduto != null)
            {
                ofertaBanco.IdProduto = oferta.IdProduto;    
            }

            if (oferta.IdProduto != null)
            {
                ofertaBanco.Titulo = oferta.Titulo;
            }

            if (oferta.IdProduto != null)
            {
                ofertaBanco.DataOferta = oferta.DataOferta;
            }

            if (oferta.IdProduto != null)
            {
                ofertaBanco.DataVencimento = oferta.DataVencimento;
            }

            if (oferta.Preco != null)
            {
                ofertaBanco.Preco = oferta.Preco;
            }

            if (oferta.Quantidade != null) 
            {
                ofertaBanco.Quantidade = oferta.Quantidade;
            }

            _context.Entry(ofertaBanco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var oferta_valido = await _context.Oferta.FindAsync(id);

                if (oferta_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oferta>> Delete(int id)
        {
            var oferta = await _context.Oferta.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            _context.Oferta.Remove(oferta);
            await _context.SaveChangesAsync();

            return oferta;
        }

    }
}