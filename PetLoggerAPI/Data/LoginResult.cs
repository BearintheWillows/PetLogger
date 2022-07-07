namespace PetLoggerAPI.Data; 

public class LoginResult {
	/// <summary>
	/// True if login attempt successful, false otherwise.
	/// </summary>
	public bool Success { get; set; }

	/// <summary>
	/// Login attempt result message.
	/// </summary>
	public string Message { get; set; } = null!;
	
	/// <summary>
	/// JWT token if login successful, null otherwise.
	/// </summary>
	public string? Token { get; set; } = null!;
	
}