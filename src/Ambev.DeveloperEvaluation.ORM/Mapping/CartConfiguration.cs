﻿using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.ToTable(nameof(Cart));	
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.HasIndex(x => x.Id);
			builder.HasMany<CartItem>(c => c.Products).WithOne().HasForeignKey(ct => ct.CartId);
			builder.HasOne<User>().WithMany().HasForeignKey(ct => ct.UserId).IsRequired();
			builder.Property(x => x.Date);
		}
	}
}
