using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //  definimos nossa rota do controller e dizemos que e um controller de api
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

       VallerContext _contexto = new VallerContext();

       // GET:  api/Reserva
       [HttpGet]
       public async Task<ActionResult<List<Reserva>>> get()
       {
            var Reservas = await _contexto.Reserva.Include(O => O.IdOfertaNavigation ).Include(U => U.IdUsuarioNavigation).ToListAsync();

            if (Reservas == null){
                return NotFound();
            }

            return Reservas;
       }
         // GET:  api/Reserva/2
       [HttpGet("{id}")]
       public async Task<ActionResult<Reserva>> get(int id)
       {

            //findfasync = procurar algo especifico     
            var Reserva = await _contexto.Reserva.Include(O => O.IdOfertaNavigation ).Include(U => U.IdUsuarioNavigation).FirstOrDefaultAsync(e => e.IdReserva == id);

            if (Reserva == null){
                return NotFound();
            }

            return Reserva;
       }
        // post api/Reserva
        [HttpPost]
        public async Task<ActionResult<Reserva>> post (Reserva Reserva){

            try{
                // tratamos contra ataques de sql injection 
                await _contexto.AddAsync(Reserva);
                // salvamos efetivamente o nosso objeto  no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }

         return Reserva;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Reserva Reserva){
            // se o id do objeto nao existir 
            // ele retorna
            
            if(id != Reserva.IdReserva){
                return BadRequest();
            }
            _contexto.Entry(Reserva).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                // verificamos se o objeto inserido realmente existe no banco
                var Reserva_valido = await _contexto.Reserva.FindAsync(id);

                if(Reserva_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
                 
            }
            // nocontent = retornar 204 ,sem nada
             return NoContent();
                
                
        }

        //  
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reserva>> Delete(int id){
             var Reserva = await _contexto.Reserva.FindAsync(id);

             if(Reserva == null){
                 return NotFound();

        }
        _contexto.Reserva.Remove(Reserva);
        await _contexto.SaveChangesAsync();

        return Reserva;

        }
    }
}