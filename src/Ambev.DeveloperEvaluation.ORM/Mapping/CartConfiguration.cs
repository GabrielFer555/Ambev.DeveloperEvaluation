namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable(nameof(Cart));	
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasIndex(x => x.Id);
			builder.HasMany<CartItem>(c => c.Products).WithOne().HasForeignKey(ct => ct.CartId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.HasOne<User>().WithMany().HasForeignKey(ct => ct.UserId).IsRequired()
							.OnDelete(DeleteBehavior.Restrict);
			builder.Property(x => x.Date);
		}
	}
}
