namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);
        builder.ComplexProperty(u => u.Name, nameBuilder =>
        {
            nameBuilder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
			nameBuilder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
		});
        builder.ComplexProperty(u => u.Address, addressBuilder =>
        {
            addressBuilder.Property(u => u.Street).HasMaxLength(50);
			addressBuilder.Property(u => u.City).HasMaxLength(30);
			addressBuilder.Property(u => u.Number).HasMaxLength(10);
			addressBuilder.Property(u => u.ZipCode).HasMaxLength(50);
			addressBuilder.Property(u => u.Long).HasMaxLength(50);
			addressBuilder.Property(u => u.Lat).HasMaxLength(50);
		});
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

    }
}
