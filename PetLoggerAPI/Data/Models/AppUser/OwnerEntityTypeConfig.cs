namespace PetLoggerAPI.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OwnerEntityTypeConfig : IEntityTypeConfiguration<Owner> {
	public void Configure(EntityTypeBuilder<Owner> builder) {
		builder.ToTable( "Owners" );
		builder.HasKey( o => o.Id );
		builder.Property( o => o.Id );
	}
}