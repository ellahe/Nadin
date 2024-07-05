using AutoMapper;
using Domain;
using Infrastructure;
using MediatR;

namespace ApplicationService.Products
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public ProductRequest ProductRequest { get; set; }
        public string UserId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductRequest);
            product.CreatedBy = request.UserId;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }
    }

}
