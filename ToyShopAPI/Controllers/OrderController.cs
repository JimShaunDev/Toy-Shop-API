#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Data;
using ToyShopAPI.Models;

namespace ToyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetOrders()
        {
            //we dont waant to return just orders, we want data we can understand
            //return await _context.Orders.ToListAsync();

            //var data = await _context.Orders.Include("OrderDetails").Include("Products").ToListAsync();
            return data;

             
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderModel(int id)
        {
            var orderModel = await _context.Orders.FindAsync(id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return orderModel;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, OrderModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel(CreateOrderModel orderRequest)
        {
            var user = await _userManager.FindByIdAsync(orderRequest.userId);

            OrderModel order = new OrderModel
            {
                UserId = user.Id.ToString(),
                OrderDate = DateTime.Now,
                IsReady = false,
                Collected = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            

            foreach (var item in orderRequest.Items)
            {
                var details = new OrderDetailsModel
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    quantity = item.Quantity
                };

                _context.Add(details);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderModel", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
