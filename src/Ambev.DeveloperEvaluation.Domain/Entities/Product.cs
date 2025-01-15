


namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class Product:Entity<int>
	{
        public decimal Price { get; private set; }
        public string Title { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string Category { get; private set; } = default!;
        public string Image { get; private set; } = default!;
        public ProductRating ProductRating { get; private set; } = default!;

        private Product() { }
		private Product(decimal price, string title, string description, string category, string image, ProductRating productRating)
        {
            Price = price;
            Title = title;
            Description = description;
            Category = category;
            Image = image;
			ProductRating = productRating;
		}

        public static Product Create(decimal price, string title, string description, string category, string image, ProductRating productRating)
        {
            ArgumentException.ThrowIfNullOrEmpty(title);
			ArgumentException.ThrowIfNullOrEmpty(category);
            
			return new Product
            {
                Price = price,
                Title = title,
                Description = description,
                Category = category,
                Image = image,
                ProductRating = productRating
            };
        }
        public void SetProductRate(ProductRating rating) {
            ProductRating = rating;
        }
        
        public void Update(decimal price, string title, string description, string category, string image, ProductRating productRating)
        {
			ArgumentException.ThrowIfNullOrEmpty(title);
			ArgumentException.ThrowIfNullOrEmpty(category);

			ProductRating = productRating;
            Price = price;
            Title = title;
            Description = description;
            Category = category;
            Image = image;

		}
    }
}
