namespace PetLoggerAPI.Data.Models;

public class Owner {
	
	/// <summary>
	/// Owners Id
	/// Unique Primary Key
	/// </summary>
	public int               Id        { get; set; }
	
	/// <summary>
	/// Owners First Name
	/// </summary>
	public string            FirstName { get; set; }
	
	/// <summary>
	/// Owners Last Name
	/// </summary>
	public string            LastName  { get; set; }
	
	/// <summary>
	/// A Collection of Pets associated with User.
	/// </summary>
	public ICollection<Pet>? Pets { get; set; } = null;

}