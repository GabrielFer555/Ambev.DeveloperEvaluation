namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartCommand, Domain.Aggregates.Cart>();
            CreateMap<Domain.Aggregates.Cart, CreateCartResult>();

        }
    }
}
