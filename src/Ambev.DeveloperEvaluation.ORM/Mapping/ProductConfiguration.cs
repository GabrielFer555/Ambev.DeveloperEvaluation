using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x=> x.Id).ValueGeneratedOnAdd();
			builder.Property(x=> x.Description).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Title).HasMaxLength(60).IsRequired();
			builder.Property(x => x.Category).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Image).HasMaxLength(500);
			builder.ComplexProperty(x => x.ProductRating, ratingBuilder => {
				ratingBuilder.Property(prop => prop.Rating);
				ratingBuilder.Property(prop => prop.Count);
			});
		}
	}
}
