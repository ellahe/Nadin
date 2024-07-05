using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.Products
{
    public class GetProductsQuery : IRequest<List<ProductResponse>>
    {
        public string CreatedBy { get; set; }
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductResponse>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.CreatedBy))
            {
                query = query.Where(p => p.CreatedBy == request.CreatedBy);
            }

            var products = await query.ToListAsync();
            return _mapper.Map<List<ProductResponse>>(products);
        }
    }

}
