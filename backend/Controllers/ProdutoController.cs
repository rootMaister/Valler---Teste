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
    public class ProdutoController : ControllerBase
    {

       VallerContext _contexto = new VallerContext();

       // GET:  api/Produto
       [HttpGet]
       public async Task<ActionResult<List<Produto>>> get()
       {
            var Produtos = await _contexto.Produto.Include(C => C.IdCategoriaNavigation).Include(U=> U.IdUsuarioNavigation).Include(O => O.Oferta).ToListAsync();

            if (Produtos == null){
                return NotFound();
            }

            return Produtos;
       }
         // GET:  api/Produto/2
       [HttpGet("{id}")]
       public async Task<ActionResult<Produto>> get(int id)
       {

            //findfasync = procurar algo especifico     
            var Produto = await _contexto.Produto.Include(C => C.IdCategoriaNavigation).Include(U=> U.IdUsuarioNavigation).Include(O => O.Oferta).FirstOrDefaultAsync(e => e.IdProduto == id);

            if (Produto == null){
                return NotFound();
            }

            return Produto;
       }
        // post api/Produto
        [HttpPost]
        public async Task<ActionResult<Produto>> post (Produto Produto){

            try{
                // tratamos contra ataques de sql injection 
                await _contexto.AddAsync(Produto);
                // salvamos efetivamente o nosso objeto  no banco de dados
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }

         return Produto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Produto Produto){
            // se o id do objeto nao existir 
            // ele retorna
            
            if(id != Produto.IdProduto){
                return BadRequest();
            }
            _contexto.Entry(Produto).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

                // verificamos se o objeto inserido realmente existe no banco
                var Produto_valido = await _contexto.Produto.FindAsync(id);

                if(Produto_valido == null){
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
        public async Task<ActionResult<Produto>> Delete(int id){
             var Produto = await _contexto.Produto.FindAsync(id);

             if(Produto == null){
                 return NotFound();

        }
        _contexto.Produto.Remove(Produto);
        await _contexto.SaveChangesAsync();

        return Produto;

        }
    }
}