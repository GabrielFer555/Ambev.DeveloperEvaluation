﻿using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
	{
		public void Configure(EntityTypeBuilder<CartItem> builder)
		{
			builder.ToTable(nameof(CartItem));
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Quantity).IsRequired();
			builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
		}
	}
}
