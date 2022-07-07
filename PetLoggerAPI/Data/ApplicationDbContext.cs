namespace PetLoggerAPI.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext : IdentityDbContext<AppUser>{

	public ApplicationDbContext() { }
	public ApplicationDbContext(DbContextOptions options) : base(options) { }

	public DbSet<Pet>   Pets  => Set<Pet>();
	public DbSet<AppUser> Owner => Set<AppUser>();
	
	/// <summary>
	/// Override OnModelCreating
	/// </summary>
	/// <param name="modelBuilder"></param>
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating( modelBuilder );
		
		modelBuilder.ApplyConfigurationsFromAssembly( typeof(ApplicationDbContext).Assembly );
	}
}