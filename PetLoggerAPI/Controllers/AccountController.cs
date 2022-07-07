namespace PetLoggerAPI.Controllers;

using System.IdentityModel.Tokens.Jwt;
using Data;
using Data.Handlers;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : BaseAPIController {

	private readonly ApplicationDbContext _context;
	private readonly UserManager<AppUser> _userManager;
	private readonly JwtHandler           _jwtHandler;

	public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, JwtHandler jwtHandler) {
		_context = context;
		_userManager = userManager;
		_jwtHandler = jwtHandler;
	}

	[HttpPost( "login" )]
	public async Task<IActionResult> Login(LoginDTO loginDTO) {
		var user = await _userManager.FindByNameAsync( loginDTO.Email );

		if ( user == null || !await _userManager.CheckPasswordAsync( user, loginDTO.Password ) ) {
			return Unauthorized( new LoginResult() { Success = false, Message = "Invalid Email or Password" } );
		}

		;
		var secToken = await _jwtHandler.GetTokenAsync( user );

		var jwt = new JwtSecurityTokenHandler().WriteToken( secToken );

		return Ok( new LoginResult() { Success = true, Message = "Login Successful", Token = jwt } );
	}
}