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
    public class DeleteArtPiecesTests
    {
        private ArtPieceService _artPieceService;
        private ArtDBContext _context;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ArtDBContext>()
                              .UseInMemoryDatabase(databaseName: "TestDatabase_DeleteTests")
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
        public async Task DeleteArtPiece_ShouldRemoveArtPiece_WhenValidIdIsProvided()
        {
            var artPieceDto = new ArtPieceDto
            {
                Title = "Mona Lisa",
                Artist = "Leonardo da Vinci",
                Year = 1503,
                Category = "Renaissance",
                Material = "Oil on Panel",
                Size = "77 cm × 53 cm",
                Price = 2000
            };

            var addedArtPiece = await _artPieceService.AddArtPiece(artPieceDto);
            var artPieceId = _context.ArtPieces.First().Id;

            var result = await _artPieceService.DeleteArtPiece(artPieceId);

            Assert.IsNull(await _context.ArtPieces.FindAsync(artPieceId));
        }

        [Test]
        public async Task  DeleteArtPiece_ShouldThrowException_WhenArtPieceIdDoesNotExist()
        {
            var nonExistentId = 999;
            
            var result = await _artPieceService.DeleteArtPiece(nonExistentId);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteArtPiece_ShouldDecreaseTotalArtPiecesCount_WhenArtPieceIsDeleted()
        {
            var artPiece1 = new ArtPieceDto { 
                Title = "Mona Lisa",
                Artist = "Leonardo da Vinci",
                Year = 1503,
                Category = "Renaissance",
                Material = "Oil on Panel",
                Size = "77 cm × 53 cm",
                Price = 2000
            };
            var artPiece2 = new ArtPieceDto { 
                Title = "Starry Night",
                Artist = "Vincent van Gogh",
                Year = 1889,
                Category = "Renaissance",
                Material = "Oil",
                Size = "50 cm x 50 cm",
                Price = 1000
            };

            await _artPieceService.AddArtPiece(artPiece1);
            await _artPieceService.AddArtPiece(artPiece2);

            var initialCount = _context.ArtPieces.Count();
            var artPieceId = _context.ArtPieces.First().Id;

            await _artPieceService.DeleteArtPiece(artPieceId);

            Assert.AreEqual(initialCount - 1, _context.ArtPieces.Count());
        }

        [Test]
        public async Task DeleteArtPiece_ShouldNotAffectOtherArtPieces()
        {
            var artPiece1 = new ArtPieceDto
            {
                Title = "The Persistence of Memory",
                Artist = "Salvador Dalí",
                Year = 1931,
                Category = "Surrealism",
                Material = "Oil on Canvas",
                IsSold = false,
                Size = "24 cm × 33 cm",
                Price = 1500000
            };

            var artPiece2 = new ArtPieceDto
            {
                Title = "The Scream",
                Artist = "Edvard Munch",
                Year = 1893,
                Category = "Expressionism",
                Material = "Oil, Tempera, and Pastel on Cardboard",
                IsSold = false,
                Size = "91 cm × 73.5 cm",
                Price = 1200000
            };
            await _artPieceService.AddArtPiece(artPiece1);
            await _artPieceService.AddArtPiece(artPiece2);

            var artPieceId = _context.ArtPieces.First(ap => ap.Title == "The Persistence of Memory").Id;
            await _artPieceService.DeleteArtPiece(artPieceId);

            var remainingArtPiece = _context.ArtPieces.FirstOrDefault(ap => ap.Title == "The Scream");
            Assert.IsNotNull(remainingArtPiece);
        }
    }
}
