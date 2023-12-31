using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NorthwindSampleAPI.Models;

namespace NorthwindSampleAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class OrderController : ControllerBase
	{
		private readonly NorthwindContext _context;

		public OrderController(NorthwindContext context)
		{
			_context = context;
		}
		
		//GET: api/order
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}
		
		//GET: api/order/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(short id)
		{
			Order? order = await _context.Orders.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			return order;
		}
		
		//POST: api/order
		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			await _context.Orders.AddAsync(order);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (OrderExists(order.OrderId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
		}

		//DELETE: api/order
		[HttpDelete("{id}")]
		public async Task<ActionResult<Order>> DeleteOrder(short id)
		{
			Order? order = await _context.Orders.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		
		private bool OrderExists(short id)
		{
			return _context.Orders.Any(order => order.OrderId == id);
		}
	}
}