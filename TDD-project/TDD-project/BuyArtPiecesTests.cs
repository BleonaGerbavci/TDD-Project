using ArtCollectionOrganizer.Configurations;
using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TDD_project
{
    public class BuyArtPiecesTests
    {
        private ArtPieceService _artPieceService;
        private ArtDBContext _context;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ArtDBContext>()
                              .UseInMemoryDatabase(databaseName: "TestDatabase")
                              .Options;
            _context = new ArtDBContext(options);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfiles()));
            _mapper = mapperConfig.CreateMapper();

            _artPieceService = new ArtPieceService(_context, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task BuyArtPiece_ValidArtPiece_ShouldMarkAsSoldAndReturnSuccessMessage()
        {
            // Arrange
            var artPiece = new ArtPieceDto
            {
                Title = "Starry Night",
                Artist = "Vincent van Gogh",
                Year = 1889,
                Category = "Painting",
                Material = "Oil on Canvas",
                IsSold = false,
                Size = "Large",
                Price = 1000000.00
            };
            await _artPieceService.AddArtPiece(artPiece);

            var savedArtPiece = await _context.ArtPieces.FirstAsync();

            // Act
            var result = await _artPieceService.BuyArtPiece(savedArtPiece.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.AreEqual($"Art piece '{savedArtPiece.Title}' by {savedArtPiece.Artist} was successfully purchased!", okResult.Value);

            var updatedArtPiece = await _context.ArtPieces.FindAsync(savedArtPiece.Id);
            Assert.IsTrue(updatedArtPiece.IsSold);
        }

        [Test]
        public async Task BuyArtPiece_AlreadySoldArtPiece_ShouldReturnErrorMessage()
        {
            // Arrange
            var artPiece = new ArtPieceDto
            {
                Title = "Mona Lisa",
                Artist = "Leonardo da Vinci",
                Year = 1503,
                Category = "Painting",
                Material = "Oil on Panel",
                IsSold = true,
                Size = "Medium",
                Price = 8000000.00
            };
            await _artPieceService.AddArtPiece(artPiece);

            var savedArtPiece = await _context.ArtPieces.FirstAsync();

            // Act
            var result = await _artPieceService.BuyArtPiece(savedArtPiece.Id);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Art piece is already sold!", badRequestResult.Value);
        }

        [Test]
        public async Task BuyArtPiece_NonExistentArtPiece_ShouldReturnErrorMessage()
        {
            // Act
            var result = await _artPieceService.BuyArtPiece(999);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Art piece does not exist!", notFoundResult.Value);
        }
    }
}
