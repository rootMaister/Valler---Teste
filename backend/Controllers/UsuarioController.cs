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
    public class UsuarioController : ControllerBase
    {
        
        VallerContext _contexto = new VallerContext();

        // GET: api/Usuario

        /// <summary>
        /// Listar Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){

            //Include("") = Adiciona efetivamente a árvore de objetos 
            var usuario = await _contexto.Usuario.Include(c =>c.Endereco).Include(c => c.Produto).Include(c => c.Reserva).Include(c => c.Telefone).ToListAsync();

            if (usuario == null){
                return NotFound();
            }

            return usuario;
        }

        
        // GET: api/Usuario/2

        /// <summary>
        /// Pegamos usuario pelo ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>>Get(int id){

            //  FindAsync = procura algo específico no banco 
            var usuario = await _contexto.Usuario.Include(c => c.Reserva).Include(c => c.Telefone).Include(c => c.Produto).Include(c => c.Reserva).FirstOrDefaultAsync(e => e.IdUsuario == id);

            if (usuario == null){
                return NotFound();
            }

            return usuario;
        }

        // POST api/Usuario

        /// <summary>
        /// Cadastrar Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){

            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamente o nosso objeto no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            
            
            return usuario;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( int id, Usuario usuario){

            //Se o Id do objeto não existir 
            //ele retorna erro 400
            if(id != usuario.IdUsuario){
                return BadRequest();
            }

            // comparamos os atributos que foram mdeficados através do EF
            _contexto.Entry(usuario).State = EntityState.Modified;


            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                //Verificamos se o objeto inserido realmente existe no banco
                var usuario_valido = await _contexto.Usuario.FindAsync(id);

                if(usuario_valido == null){
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
        public async Task<ActionResult<Usuario>> Delete(int id){

            var usuario = await _contexto.Usuario.FindAsync(id);
            if(usuario == null){
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

    }
}










