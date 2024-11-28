using ArtCollectionOrganizer.Configurations;
using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TDD_project
{
    public class SearchArtPiecesTests
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
        public async Task SearchArtPieces_ShouldReturnEmpty_WhenNoMatchFound()
        {
            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Mona Lisa",
                Artist = "Leonardo da Vinci",
                Year = 1503,
                Category = "Portrait",
                Material = "Oil on Canvas",
                Size = "77 cm × 53 cm",
                Price = 1000000,
                IsSold = false
            });

            var result = await _artPieceService.SearchArtPieces("Nonexistent");

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task FilterArtPieces_ShouldReturnPiecesByCategory()
        {
            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Painting 1",
                Artist = "Artist X",
                Category = "Painting",
                Material = "Canvas",  
                Size = "30x40",      
                Price = 500,    
                Year = 2020,
                IsSold = false
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Sculpture 1",
                Artist = "Artist Y",
                Category = "Sculpture",
                Material = "Bronze",
                Size = "50x50",
                Price = 1500,
                Year = 2021,
                IsSold = true
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Painting 2",
                Artist = "Artist Z",
                Category = "Painting",
                Material = "Wood",
                Size = "40x50",
                Price = 700,
                Year = 2022,
                IsSold = false
            });

            var paintings = await _artPieceService.FilterArtPieces("Painting");

            Assert.AreEqual(2, paintings.Count);
        }

        [Test]
        public async Task FilterArtPieces_ShouldReturnEmpty_WhenNoPiecesMatchCategoryAndArtist()
        {
            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Abstract Painting",
                Artist = "Artist X",
                Category = "Painting",
                Material = "Oil on Canvas",
                Size = "100 cm × 80 cm",
                Price = 1500,
                IsSold = false,
                Year = 2020
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Modern Sculpture",
                Artist = "Artist Y",
                Category = "Sculpture",
                Material = "Marble",
                Size = "150 cm × 120 cm",
                Price = 5000,
                IsSold = false,
                Year = 2015
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Portrait Painting",
                Artist = "Artist X",
                Category = "Painting",
                Material = "Oil on Canvas",
                Size = "90 cm × 70 cm",
                Price = 2000,
                IsSold = false,
                Year = 2021
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Landscape Painting",
                Artist = "Artist Z",
                Category = "Painting",
                Material = "Watercolor",
                Size = "80 cm × 60 cm",
                Price = 1200,
                IsSold = false,
                Year = 2018
            });

            var filteredResults = (await _artPieceService.FilterArtPieces("Sculpture"))
                .Where(art => art.Artist == "Artist X").ToList();

            Assert.IsEmpty(filteredResults);
        }

        [Test]
        public async Task FilterArtPieces_ShouldReturnEmpty_WhenCategoryDoesNotExist()
        {
            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Starry Night",
                Artist = "Vincent van Gogh",
                Category = "Painting",
                Material = "Oil on Canvas",
                Size = "73.7 cm × 92.1 cm",
                Price = 10000,
                IsSold = false,
                Year = 1889
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "David",
                Artist = "Michelangelo",
                Category = "Sculpture",
                Material = "Marble",
                Size = "200 cm × 150 cm",
                Price = 25000,
                IsSold = false,
                Year = 1504
            });

            await _artPieceService.AddArtPiece(new ArtPieceDto
            {
                Title = "Sunflowers",
                Artist = "Vincent van Gogh",
                Category = "Painting",
                Material = "Oil on Canvas",
                Size = "92 cm × 73 cm",
                Price = 15000,
                IsSold = false,
                Year = 1888
            });

            var nonexistentCategoryResults = await _artPieceService.FilterArtPieces("Photography");

            Assert.IsEmpty(nonexistentCategoryResults);
        }

    }
}