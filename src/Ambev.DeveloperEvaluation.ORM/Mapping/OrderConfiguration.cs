namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");
			builder.HasKey(x=> x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasOne<User>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();
			builder.HasMany<OrderItem>(c => c.Items).WithOne().HasForeignKey(x => x.OrderId).IsRequired();
			builder.Property(u => u.OrderStatus)
			.HasConversion<string>()
			.HasMaxLength(30);
			builder.Ignore(x => x.TotalPrice);
			builder.Property(x => x.Branch).IsRequired();
			builder.Property(x => x.CreatedAt).IsRequired();
			builder.Ignore(x => x.TotalPrice);
			builder.Ignore(x => x.TotalAmountDiscount);
		}
	}
}
