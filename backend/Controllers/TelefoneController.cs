using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Controllers;
using Newtonsoft.Json;

// Adiciona a arvore de objetos 
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson


namespace backend.Controllers
{
    // Define a rota do controller, e diz que é um controller de API
    [Route("api/[controller]")] 
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        VallerContext _contexto = new VallerContext();

        // GET: api/Telefone

        /// <summary>
        /// Listar Telefones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Telefone>>> Get()
        {
            var telefones = await _contexto.Telefone.Include(c => c.IdUsuarioNavigation).ToListAsync();

            if(telefones == null) {
                return NotFound();
            }

            return telefones;
        }
        
        // GET: api/Telefone/2

        /// <summary>
        /// Chamar Telefone pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefone>> Get(int id)
        {
            var telefone = await _contexto.Telefone.Include(c => c.IdUsuarioNavigation).FirstOrDefaultAsync(e => e.IdTelefone == id);

            if(telefone == null) {
                return NotFound();
            }

            return telefone;
        }

        // POST: api/Telefone

        /// <summary>
        /// Cadastrar Telefone
        /// </summary>
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Telefone>> Post(Telefone telefone)
        {
            try
            {
                // Tratamento contra SQL Injection
                await _contexto.AddAsync(telefone);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return telefone;
        }

        // PUT

        /// <summary>
        /// Editar Telefone pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Telefone telefone)
        {
            if(id != telefone.IdTelefone){
                return BadRequest();
            }

            // Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(telefone).State = EntityState.Modified;
            
            try {
                await _contexto.SaveChangesAsync(); 
            } catch (DbUpdateConcurrencyException) {
                // Verfica se o objeto inserido existe no banco
                var evento_valido = await _contexto.Telefone.FindAsync(id);

                if(evento_valido == null) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/telefone/id

        /// <summary>
        /// Deletar Telefone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Telefone>> Delete(int id){
            var telefone = await _contexto.Telefone.FindAsync(id);

            if(telefone == null) {
                return NotFound();
            }

            _contexto.Telefone.Remove(telefone);
            await _contexto.SaveChangesAsync();
            
            return telefone;
        }
    }
}