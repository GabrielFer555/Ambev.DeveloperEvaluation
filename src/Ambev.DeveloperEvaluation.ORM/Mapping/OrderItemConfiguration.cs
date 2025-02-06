namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable("OrderItems");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Quantity).IsRequired();
			builder.Property(x => x.Price).IsRequired();
			builder.Property(x => x.Discount).IsRequired();
			builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId)
				.OnDelete(DeleteBehavior.Restrict);
			builder.Property(u => u.OrderItemStatus)
			.HasConversion<string>()
			.HasMaxLength(30);
			builder.Ignore(x => x.TotalPrice);
		}
	}
}
