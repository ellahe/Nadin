using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.ApplicationService;
using ProductAPI.Controllers;
using Moq;
using Xunit;
using System.Security.Claims;
using ApplicationService.Products;

namespace ProductAPI.Test
{
    public class ProductTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult()
        {
            // Arrange
            var productRequest = new ProductRequest
            {
                Name = "Product1",
                IsAvailable = true,
                ManufactureEmail = "test@example.com",
                ManufacturePhone = "123456789",
                ProduceDate = DateTime.UtcNow
            };

            var userId = "test-user-id";
            _userManagerMock.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new ProductResponse());

            // Act
            var result = await _controller.CreateProduct(productRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProductResponse>(okResult.Value);
        }
    }

}
