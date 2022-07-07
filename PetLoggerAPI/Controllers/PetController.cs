namespace PetLoggerAPI.Controllers;

using Data;
using Data.Models;
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

	[HttpGet( "{id}" )]
	public async Task<ActionResult<Pet>> GetPet(int id) {
		if ( _context.Pets == null ) {
			return NotFound();
		}
		var pet = await _context.Pets.FindAsync( id );
		
		if ( pet == null ) {
			return NotFound();
		}

		return pet;
	}


	[HttpPost]
	public async Task<ActionResult<Pet>> PostPet(Pet pet) {
		if ( _context.Pets == null ) {
			return Problem( "Entity set 'ApplicationDbContext.Pets' is null" );
		} ;
		_context.Pets.Add(pet);
		await _context.SaveChangesAsync();
	
		return CreatedAtAction( "GetPet", new {id = pet.Id }, pet );
	}
}