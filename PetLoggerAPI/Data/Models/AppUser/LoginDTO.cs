namespace PetLoggerAPI.Data.Models;

using System.ComponentModel.DataAnnotations;

public class LoginDTO {
	[Required( ErrorMessage = "Username is required" )]
	public string Email { get; set; } = null!;
	
	[Required( ErrorMessage = "Password is required" )]
	public string Password { get; set; } = null!;
}