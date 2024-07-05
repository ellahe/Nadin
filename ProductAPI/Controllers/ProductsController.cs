using ApplicationService.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.ApplicationService;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            var userId = _userManager.GetUserId(User);
            var command = new CreateProductCommand
            {
                ProductRequest = productRequest,
                UserId = userId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string createdBy)
        {
            var query = new GetProductsQuery
            {
                CreatedBy = createdBy
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }

}
