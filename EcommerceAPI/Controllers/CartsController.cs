using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly dbContext _context;
        private readonly IDataHelperCart<Cart> data;

        public CartsController(dbContext context, IDataHelperCart<Cart> data)
        {
            _context = context;
            this.data = data;
        }

        // GET: api/Carts
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<Cart>>> Getcart()
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            return await _context.cart.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public ActionResult<List<CartProduct>> GetCart(int id)
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            var cart = data.GetById(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }


        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{UserEmail}")]
        [Authorize]

        public async Task<ActionResult<Cart>> PostCart([FromBody]List<CartProduct> cartpro,string UserEmail)
        {
          if (_context.cart == null)
          {
              return Problem("Entity set 'dbContext.cart'  is null.");
          }
            data.AddCart(cartpro, UserEmail);

            return Ok();
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            var cart = await _context.cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            var products = _context.cartProduct.Where(i=>i.CartId==id);

            foreach (var item in products)
            {
                _context.cartProduct.Remove(item);

            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return (_context.cart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
