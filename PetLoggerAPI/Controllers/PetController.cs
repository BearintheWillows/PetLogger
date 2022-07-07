namespace PetLoggerAPI.Controllers;

using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PetController : BaseAPIController {

	private readonly ApplicationDbContext _context;
	public PetController(ApplicationDbContext context) {
		_context = context;
	}
	
	
	[HttpGet]
	public async Task<IActionResult> GetAll() {
		return Ok(await _context.Pets.ToListAsync());
	}
}