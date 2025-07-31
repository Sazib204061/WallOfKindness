using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MomotarJhuri.Application.Gifts;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Infractructure.Data;
using Moq;

namespace MomotarJhuri.UnitTests.Services
{
    public class GiftServices_CreateTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<IWebHostEnvironment> _mockEnv;
        private readonly GiftServices _giftServices;

        public GiftServices_CreateTests()
        {
            // Create options with InMemory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Create actual context for testing
            var context = new ApplicationDbContext(options);

            // Create partial mock
            _mockContext = new Mock<ApplicationDbContext>(options) { CallBase = true };
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

            _mockEnv = new Mock<IWebHostEnvironment>();
            _giftServices = new GiftServices(_mockContext.Object, _mockEnv.Object);
        }

        [Fact]
        public async Task CreateGiftWithDetailsAsync_ValidGift_SavesToDatabase()
        {
            // Arrange
            var gift = new Gift
            {
                Id = 1,
                Title = "Test Gift",
                Detail = new GiftDetail { Description = "Test Description" },
                Images = new List<Image>()
            };

            // Setup tracking for verification
            var addedGifts = new List<Gift>();
            _mockContext.Setup(c => c.Gifts.Add(It.IsAny<Gift>()))
                       .Callback<Gift>(g => addedGifts.Add(g))
                       .Returns((EntityEntry<Gift>)null);

            // Act
            await _giftServices.CreateGiftWithDetailsAsync(gift);

            // Assert
            _mockContext.Verify(c => c.Gifts.Add(It.IsAny<Gift>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            addedGifts.Should().Contain(g => g.Id == 1 && g.Title == "Test Gift");
        }

        [Fact]
        public async Task CreateGiftWithDetailsAsync_NullGift_ThrowsException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _giftServices.CreateGiftWithDetailsAsync(null));
        }
    }
}