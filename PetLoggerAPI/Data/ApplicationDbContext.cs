namespace PetLoggerAPI.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext : DbContext {

	public ApplicationDbContext() { }
	public ApplicationDbContext(DbContextOptions options) : base(options) { }

	public DbSet<Pet>   Pets  => Set<Pet>();
	public DbSet<Owner> Owner => Set<Owner>();
	
	/// <summary>
	/// Override OnModelCreating
	/// </summary>
	/// <param name="modelBuilder"></param>
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating( modelBuilder );
		
		modelBuilder.ApplyConfigurationsFromAssembly( typeof(ApplicationDbContext).Assembly );
	}
}