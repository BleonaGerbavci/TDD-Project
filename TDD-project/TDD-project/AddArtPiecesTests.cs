using ArtCollectionOrganizer.Configurations;
using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TDD_project
{
    public class AddArtPiecesTests
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
        public void AddArtPiece_ShouldAddNewArtPiece_WhenValidDataIsProvided()
        {
            var newArtPieceDto = new ArtPieceDto
            {
                Title = "Starry Night",
                Artist = "Vincent van Gogh",
                Year = 1889,
                Category = "Post-Impressionism",
                Material = "Oil on Canvas",
                IsSold = false,
                Size = "73.7 cm × 92.1 cm",
                Price =1000 
            };

            var result = _artPieceService.AddArtPiece(newArtPieceDto);

            Assert.IsTrue(result.Result is OkObjectResult);
        }


        [Test]
        public void AddArtPiece_ShouldThrowException_WhenRequiredFieldsAreMissing()
        {
            var incompleteArtPiece = new ArtPieceDto { Artist = "Pablo Picasso" };

            var result = _artPieceService.AddArtPiece(incompleteArtPiece).Result;

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task AddArtPiece_ShouldFail_WhenDuplicateTitleAndArtistIsProvided()
        {
            var artPiece = new ArtPieceDto { Title = "The Starry Night", Artist = "Vincent van Gogh" };
            await _artPieceService.AddArtPiece(artPiece);

            var duplicateArtPiece = new ArtPieceDto { Title = "The Starry Night", Artist = "Vincent van Gogh" };

            var result = await _artPieceService.AddArtPiece(duplicateArtPiece);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void AddArtPiece_ShouldThrowException_WhenInvalidYearIsProvided()
        {
            var futureArtPiece = new ArtPieceDto { 
                Title = "Future Art", 
                Artist = "New Artist", 
                Year = DateTime.Now.Year + 1,
                Category = "Post-Impressionism",
                Material = "Oil on Canvas",
                IsSold = false,
                Size = "73.7 cm × 92.1 cm",
                Price = 1000
            };

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _artPieceService.AddArtPiece(futureArtPiece));
        }


        [Test]
        public void AddArtPiece_ShouldThrowException_WhenTitleExceedsMaxLength()
        {
            var longTitleArtPiece = new ArtPieceDto
            {
                Title = new string('A', 101),
                Artist = "Famous Artist"
            };

            var result = _artPieceService.AddArtPiece(longTitleArtPiece).Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
