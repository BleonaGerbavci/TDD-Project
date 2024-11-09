using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;

namespace TDD_project
{
    public class DeleteArtPiecesTests
    {
        private ArtPieceService _artPieceService;

        [SetUp]
        public void SetUp()
        {
            _artPieceService = new ArtPieceService();
        }

        [Test]
        public void DeleteArtPiece_ShouldRemoveArtPiece_WhenValidIdIsProvided()
        {
            var artPiece = new ArtPiece { Title = "Mona Lisa", Artist = "Leonardo da Vinci", Year = 1503 };
            _artPieceService.AddArtPiece(artPiece);

            var result = _artPieceService.DeleteArtPiece(artPiece.Id);

            Assert.IsTrue(result);
            Assert.IsNull(_artPieceService.GetArtPieceById(artPiece.Id));
        }

        [Test]
        public void DeleteArtPiece_ShouldThrowException_WhenArtPieceIdDoesNotExist()
        {

            var nonExistentArtPieceId = 999;

            Assert.Throws<KeyNotFoundException>(() => _artPieceService.DeleteArtPiece(nonExistentArtPieceId));
        }

        [Test]
        public void DeleteArtPiece_ShouldDecreaseTotalArtPiecesCount_WhenArtPieceIsDeleted()
        {
            // Arrange
            var artPiece1 = new ArtPiece { Title = "Mona Lisa", Artist = "Leonardo da Vinci", Year = 1503 };
            var artPiece2 = new ArtPiece { Title = "Starry Night", Artist = "Vincent van Gogh", Year = 1889 };
            _artPieceService.AddArtPiece(artPiece1);
            _artPieceService.AddArtPiece(artPiece2);

            // Act
            var initialCount = _artPieceService.GetAllArtPieces().Count;
            _artPieceService.DeleteArtPiece(artPiece1.Id);

            // Assert
            Assert.AreEqual(initialCount - 1, _artPieceService.GetAllArtPieces().Count);
        }

        [Test]
        public void DeleteArtPiece_ShouldNotAffectOtherArtPieces()
        {
            var artPiece1 = new ArtPiece { Title = "The Persistence of Memory", Artist = "Salvador Dalí", Year = 1931 };
            var artPiece2 = new ArtPiece { Title = "The Scream", Artist = "Edvard Munch", Year = 1893 };
            _artPieceService.AddArtPiece(artPiece1);
            _artPieceService.AddArtPiece(artPiece2);

            _artPieceService.DeleteArtPiece(artPiece1.Id);

            var remainingArtPiece = _artPieceService.GetArtPieceById(artPiece2.Id);
            Assert.IsNotNull(remainingArtPiece);
        }
    }
}