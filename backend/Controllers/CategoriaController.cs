using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        VallerContext _context = new VallerContext();

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get() {

            var categorias = await _context.Categoria.ToListAsync();

            if(categorias == null) {
                return NotFound();
            }
            return categorias;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id) {

            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null ) {
                return NotFound();
            }
            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria) {
            
            try {
                await _context.Categoria.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            return categoria;
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Categoria categoria){

            if(id != categoria.IdCategoria) {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                var categoria_valido = _context.Categoria.FindAsync(id);
                if(categoria_valido == null) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id){

            var categoria = await _context.Categoria.FindAsync(id);
            
            if(categoria == null){
                return NotFound();
            }
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}