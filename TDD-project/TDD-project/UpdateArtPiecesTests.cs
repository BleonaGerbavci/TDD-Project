/*using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;

namespace TDD_project
{
    public class UpdateArtPiecesTests
    {
        private ArtPieceService _artPieceService;

        [SetUp]
        public void SetUp()
        {
            _artPieceService = new ArtPieceService();
        }

        [Test]
        public void UpdateArtPiece_ShouldUpdateExistingArtPiece_WhenValidDataIsProvided()
        {
            var existingArtPiece = new ArtPiece { Title = "The Persistence of Memory", Artist = "Salvador Dalí", Year = 1931 };
            _artPieceService.AddArtPiece(existingArtPiece);

            existingArtPiece.Year = 1934;
            var result = _artPieceService.UpdateArtPiece(existingArtPiece);

            Assert.IsTrue(result); 
            Assert.AreEqual(1934, _artPieceService.GetArtPieceByTitle("The Persistence of Memory").Year);
        }

        [Test]
        public void UpdateArtPiece_ShouldThrowException_WhenArtPieceDoesNotExist()
        {

            var nonExistentArtPiece = new ArtPiece { Title = "Nonexistent", Artist = "Unknown Artist", Year = 2000 };

            Assert.Throws<KeyNotFoundException>(() => _artPieceService.UpdateArtPiece(nonExistentArtPiece));
        }

        [Test]
        public void UpdateArtPiece_ShouldUpdateArtist_WhenOnlyArtistIsChanged()
        {
            var existingArtPiece = new ArtPiece { Title = "Starry Night", Artist = "Vincent van Gogh", Year = 1889 };
            _artPieceService.AddArtPiece(existingArtPiece);

            existingArtPiece.Artist = "Van Gogh, Vincent";
            var result = _artPieceService.UpdateArtPiece(existingArtPiece);

            Assert.IsTrue(result);
            Assert.AreEqual("Van Gogh, Vincent", _artPieceService.GetArtPieceByTitle("Starry Night").Artist);
        }

        [Test]
        public void UpdateArtPiece_ShouldThrowException_WhenInvalidYearIsProvided()
        {
            var existingArtPiece = new ArtPiece { Title = "The Persistence of Memory", Artist = "Salvador Dalí", Year = 1931 };
            _artPieceService.AddArtPiece(existingArtPiece);

            existingArtPiece.Year = DateTime.Now.Year + 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => _artPieceService.UpdateArtPiece(existingArtPiece));
        }
    }
}*/