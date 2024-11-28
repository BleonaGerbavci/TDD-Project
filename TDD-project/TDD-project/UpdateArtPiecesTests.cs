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
    public class UpdateArtPiecesTests
    {
        private ArtPieceService _artPieceService;
        private ArtDBContext _context;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ArtDBContext>()
                              .UseInMemoryDatabase(databaseName: "TestDatabase_UpdateTests")
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
        public async Task UpdateArtPiece_ShouldUpdateExistingArtPiece_WhenValidDataIsProvided()
        {
            var artPieceDto = new ArtPieceDto
            {
                Title = "The Persistence of Memory",
                Artist = "Salvador Dalí",
                Year = 1931,
                Category = "Surrealism",
                Material = "Oil on Canvas",
                Size = "Medium",
                Price = 1000
            };

            var result = await _artPieceService.AddArtPiece(artPieceDto);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addedArtPiece = okResult.Value as ArtPiece;
            Assert.IsNotNull(addedArtPiece);

            var updateDto = new UpdateArtPieceDto
            {
                Title = "The Persistence of Memory",
                Artist = "Salvador Dalí",
                Year = 1934
            };

            var updateResult = await _artPieceService.UpdateArtPiece(addedArtPiece.Id, updateDto);

            var updateOkResult = updateResult as OkObjectResult;
            Assert.IsNotNull(updateOkResult);
            Assert.AreEqual("Art piece was successfully updated!", updateOkResult.Value);

            var updatedArtPiece = await _context.ArtPieces.FindAsync(addedArtPiece.Id);
            Assert.AreEqual(1934, updatedArtPiece.Year);
        }

        [Test]
        public async Task UpdateArtPiece_ShouldReturnBadRequest_WhenArtPieceDoesNotExist()
        {
            var updateDto = new UpdateArtPieceDto
            {
                Title = "Nonexistent",
                Artist = "Unknown Artist",
                Year = 2000
            };

            var result = await _artPieceService.UpdateArtPiece(999, updateDto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Art piece does not exist!", badRequestResult.Value);
        }

        [Test]
        public async Task UpdateArtPiece_ShouldUpdateArtist_WhenOnlyArtistIsChanged()
        {
            var artPieceDto = new ArtPieceDto
            {
                Title = "Starry Night",
                Artist = "Vincent van Gogh",
                Year = 1889,
                Category = "Post-Impressionism",
                Material = "Oil on Canvas",
                Size = "Large",
                Price = 1500
            };

            var result = await _artPieceService.AddArtPiece(artPieceDto);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addedArtPiece = okResult.Value as ArtPiece;
            Assert.IsNotNull(addedArtPiece);

            var updateDto = new UpdateArtPieceDto
            {
                Artist = "Van Gogh, Vincent" 
            };

            var updateResult = await _artPieceService.UpdateArtPiece(addedArtPiece.Id, updateDto);

            var updateOkResult = updateResult as OkObjectResult;
            Assert.IsNotNull(updateOkResult);
            Assert.AreEqual("Art piece was successfully updated!", updateOkResult.Value);

            var updatedArtPiece = await _context.ArtPieces.FindAsync(addedArtPiece.Id);
            Assert.AreEqual("Van Gogh, Vincent", updatedArtPiece.Artist);
        }


        [Test]
        public async Task UpdateArtPiece_ShouldReturnBadRequest_WhenInvalidYearIsProvided()
        {
            var artPieceDto = new ArtPieceDto
            {
                Title = "The Persistence of Memory",
                Artist = "Salvador Dalí",
                Year = 1931,
                Category = "Surrealism",
                Material = "Oil on Canvas",
                Size = "Medium",
                Price = 1000
            };

            var result = await _artPieceService.AddArtPiece(artPieceDto);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addedArtPiece = okResult.Value as ArtPiece;
            Assert.IsNotNull(addedArtPiece);

            var updateDto = new UpdateArtPieceDto
            {
                Year = DateTime.Now.Year + 1
            };

            var updateResult = await _artPieceService.UpdateArtPiece(addedArtPiece.Id, updateDto);

            var badRequestResult = updateResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Year cannot be in the future.", badRequestResult.Value);
        }


        [Test]
        public async Task UpdateArtPiece_ShouldReturnBadRequest_WhenNegativePriceIsProvided()
        {
            var artPieceDto = new ArtPieceDto
            {
                Title = "The Persistence of Memory",
                Artist = "Salvador Dalí",
                Year = 1931,
                Category = "Surrealism",
                Material = "Oil on Canvas",
                Size = "Medium",
                Price = 1000
            };

            var result = await _artPieceService.AddArtPiece(artPieceDto);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addedArtPiece = okResult.Value as ArtPiece;
            Assert.IsNotNull(addedArtPiece);

            var updateDto = new UpdateArtPieceDto
            {
                Price = -100
            };

            var updateResult = await _artPieceService.UpdateArtPiece(addedArtPiece.Id, updateDto);

            var badRequestResult = updateResult as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Price cannot be negative.", badRequestResult.Value);
        }

    }
}
