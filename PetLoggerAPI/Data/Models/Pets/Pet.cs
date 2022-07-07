namespace PetLoggerAPI.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pet {
	

	/// <summary>
	/// ID of the pet. Primary Key.
	/// </summary>
	public int Id { get; set; }
	
	/// <summary>
	/// First Name of pet
	/// </summary>
	public string FirstName { get; set; }
	
	/// <summary>
	/// Family Name of pet. Dependent on Owners last name
	/// </summary>
	public string FamilyName { get; set; }
	
	public DateTime DateOfBirth { get; set; }
	
	/// <summary>
	/// Breed of Pet
	/// </summary>
	public string Breed { get; set; }
	
	/// <summary>
	/// Color of Pet
	/// </summary>
	public string Color { get; set; }
	
	/// <summary>
	/// Foreign Key. Owner Id.
	/// </summary>
	[ForeignKey(nameof(AppUser))]
	public int UserId { get; set; }
	
	/// <summary>
	/// Navigation Property
	/// Owner of Pet
	/// </summary>
	public AppUser User { get; set; }

	public int GetAge() {
		var now = DateTime.Now;
		var age = now.Year - DateOfBirth.Year;
		if ( now.Month < DateOfBirth.Month ||
		     ( now.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day)) {
			age--;
		}

		return age;
	}
	
	
}