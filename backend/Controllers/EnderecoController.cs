using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.Models;


//para adicionar a árvore de objeto adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace Backend.Controllers
{

    //Definimos nossa roda do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        
        VallerContext _contexto = new VallerContext();

        // GET: api/Endereco
        [HttpGet]
        public async Task<ActionResult<List<Endereco>>> Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var endereco = await _contexto.Endereco.Include(u => u.IdUsuarioNavigation).ToListAsync();

            if (endereco == null){
                return NotFound();
            }

            return endereco;
        }

        // GET: api/Endereco/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> Get(int id){

            //  FindAsync = procura algo específico no banco 
            var endereco = await _contexto.Endereco.FindAsync(id);

            if (endereco == null){
                return NotFound();
            }

            return endereco;
        }

        // POST api/Endereco
        [HttpPost]
        public async Task<ActionResult<Endereco>> Post(Endereco endereco){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(endereco);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return endereco;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Endereco endereco){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != endereco.IdEndereco){   
                return BadRequest();
            }

            // comparamos os atributos que foram mdeficados através do EF
            _contexto.Entry(endereco).State = EntityState.Modified;


            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var endereco_valido = await _contexto.Endereco.FindAsync(id);

                if(endereco_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }

            // NoContent = retorna 204, sem nada
            return NoContent();

        }

        //DELETE api/evento/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Endereco>> Delete(int id){

            var endereco = await _contexto.Endereco.FindAsync(id);
            if(endereco == null){
                return NotFound();
            }

            _contexto.Endereco.Remove(endereco);
            await _contexto.SaveChangesAsync();

            return endereco;
        }

    }
}
