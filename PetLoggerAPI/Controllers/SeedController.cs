namespace PetLoggerAPI.Controllers;

using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route( "api/[controller]/[action]" ), ApiController]

public class SeedController : BaseAPIController {
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IConfiguration            _configuration;
	private readonly ApplicationDbContext _context;
	public SeedController(
		RoleManager<IdentityRole> roleManager,
		UserManager<IdentityUser> userManager,
		IConfiguration            configuration,
		ApplicationDbContext      context
	) {
		_roleManager = roleManager;
		_userManager = userManager;
		_configuration = configuration;
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult> CreateDefaultUsers() {

	//Setup default role names
	 string role_RegisteredUser = "RegisteredUser";

	 string role_administrator = "Administrator";

		//Create default roles if they don't exist
		if ( await _roleManager.FindByNameAsync( role_RegisteredUser ) == null )
			await _roleManager.CreateAsync( new IdentityRole( role_RegisteredUser ) );

		if ( await _roleManager.FindByNameAsync( role_administrator ) == null )
			await _roleManager.CreateAsync( new IdentityRole( role_administrator ) );

			//Create list to track new users
			var addedUserList = new List<AppUser>();

		//check if the admin user exists
		var emailAdmin = "admin@email.com";

		if ( await _userManager.FindByNameAsync( emailAdmin ) == null ) {
			//create a new admin AppUser account
			var userAdmin = new AppUser {
				SecurityStamp = Guid.NewGuid().ToString(), UserName = emailAdmin, Email = emailAdmin,
			};

			//insert admin into db
			await _userManager.CreateAsync( userAdmin, _configuration[ "DefaultPasswords:Aministrator" ] );

			//Assign the "Registered User and "Administrator" roles to the admin user
			await _userManager.AddToRoleAsync( userAdmin, role_RegisteredUser );
			await _userManager.AddToRoleAsync( userAdmin, role_administrator );

			//Confirm email and remove lockout
			userAdmin.EmailConfirmed = true;
			userAdmin.LockoutEnabled = false;

			//add the admin user to the added users list
			addedUserList.Add( userAdmin ); }

		//check if the standard user exists
		var emailUser = "user@email.com";

		if ( await _userManager.FindByNameAsync( emailUser ) == null ) {
			//create new standard AppUser Account
			var userUser = new AppUser {
				SecurityStamp = Guid.NewGuid().ToString(), UserName = emailUser, Email = emailUser,
			};

			//insert standard user into db
			await _userManager.AddToRoleAsync( userUser, _configuration[ "DefaultPassword:RegisteredUser" ] );

			//Confirm email and remove lockout
			userUser.EmailConfirmed = true;
			userUser.LockoutEnabled = false;

			//add the standard user to the added users list
			addedUserList.Add( userUser );
		}

		//if we added at least one user, persist changes to db
		if ( addedUserList.Count != 0 ) 
			await _context.SaveChangesAsync();
		
		return new JsonResult( new {
				Count = addedUserList.Count,
				Users = addedUserList });
		}
	}
			