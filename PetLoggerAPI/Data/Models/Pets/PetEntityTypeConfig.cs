namespace PetLoggerAPI.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PetEntityTypeConfig : IEntityTypeConfiguration<Pet> {
	public void Configure(EntityTypeBuilder<Pet> builder) {
		builder.ToTable( "Pets" );
		builder.HasKey( p => p.Id );
		builder.Property( p => p.Id ).IsRequired();
		builder.Property( p => p.FirstName ).IsRequired();
		builder.Property( p => p.FamilyName ).IsRequired();
		builder.Property( p => p.Breed ).IsRequired();
		builder.Property( p => p.DateOfBirth ).IsRequired();
		builder
		   .HasOne( p => p.User )
		   .WithMany( p => p.Pets )
		   .HasForeignKey( p => p.UserId );
	}
}