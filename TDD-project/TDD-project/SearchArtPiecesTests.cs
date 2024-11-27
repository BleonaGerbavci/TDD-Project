/*using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;

namespace TDD_project
{
    public class SearchArtPiecesTests
    {
        private ArtPieceService _artPieceService;

        [SetUp]
        public void SetUp()
        {
            _artPieceService = new ArtPieceService();
        }

        [Test]
        public void SearchArtPieces_ShouldReturnEmpty_WhenNoMatchFound()
        {

            _artPieceService.AddArtPiece(new ArtPiece { Title = "Mona Lisa", Artist = "Leonardo da Vinci", Year = 1503 });

            var result = _artPieceService.SearchArtPieces("Nonexistent");

            Assert.IsEmpty(result);
        }

        [Test]
        public void SearchArtPieces_ShouldReturnResultsBasedOnYearRange()
        {

            _artPieceService.AddArtPiece(new ArtPiece { Title = "Piece 1", Artist = "Artist A", Year = 1900 });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Piece 2", Artist = "Artist B", Year = 1950 });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Piece 3", Artist = "Artist C", Year = 2000 });

            var results = _artPieceService.SearchArtPieces(yearStart: 1900, yearEnd: 1950);

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void FilterArtPieces_ShouldReturnPiecesByCategory()
        {
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Painting 1", Artist = "Artist X", Category = "Painting" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Sculpture 1", Artist = "Artist Y", Category = "Sculpture" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Painting 2", Artist = "Artist Z", Category = "Painting" });

            var paintings = _artPieceService.FilterArtPieces("Painting");

            Assert.AreEqual(2, paintings.Count);
        }

        [Test]
        public void FilterArtPieces_ShouldReturnEmpty_WhenNoPiecesMatchCategoryAndArtist()
        {
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Abstract Painting", Artist = "Artist X", Category = "Painting" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Modern Sculpture", Artist = "Artist Y", Category = "Sculpture" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Portrait Painting", Artist = "Artist X", Category = "Painting" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Landscape Painting", Artist = "Artist Z", Category = "Painting" });

            var filteredResults = _artPieceService.FilterArtPieces("Sculpture")
                .Where(art => art.Artist == "Artist X").ToList();

            Assert.IsEmpty(filteredResults);
        }

        [Test]
        public void FilterArtPieces_ShouldReturnEmpty_WhenCategoryDoesNotExist()
        {
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Starry Night", Artist = "Vincent van Gogh", Category = "Painting" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "David", Artist = "Michelangelo", Category = "Sculpture" });
            _artPieceService.AddArtPiece(new ArtPiece { Title = "Sunflowers", Artist = "Vincent van Gogh", Category = "Painting" });

            var nonexistentCategoryResults = _artPieceService.FilterArtPieces("Photography");

            Assert.IsEmpty(nonexistentCategoryResults);
        }

    }
}*/