using ArtCollectionOrganizer.Models;
using ArtCollectionOrganizer.Services;

namespace TDD_project
{
    public class AddArtPiecesTests
    {
        private ArtPieceService _artPieceService;

        [SetUp]
        public void SetUp()
        {
            _artPieceService = new ArtPieceService();
        }

        [Test]
        public void AddArtPiece_ShouldAddNewArtPiece_WhenValidDataIsProvided()
        {

            var newArtPiece = new ArtPiece { Title = "Starry Night", Artist = "Vincent van Gogh", Year = 1889 };

            var result = _artPieceService.AddArtPiece(newArtPiece);

            Assert.IsTrue(result);
            Assert.Contains(newArtPiece, _artPieceService.GetAllArtPieces());
        }

        [Test]
        public void AddArtPiece_ShouldThrowException_WhenRequiredFieldsAreMissing()
        {

            var incompleteArtPiece = new ArtPiece { Artist = "Pablo Picasso" };

            Assert.Throws<ArgumentException>(() => _artPieceService.AddArtPiece(incompleteArtPiece));
        }

        [Test]
        public void AddArtPiece_ShouldFail_WhenDuplicateTitleAndArtistIsProvided()
        {

            var artPiece = new ArtPiece { Title = "The Starry Night", Artist = "Vincent van Gogh" };
            _artPieceService.AddArtPiece(artPiece);

            Assert.Throws<InvalidOperationException>(() => _artPieceService.AddArtPiece(new ArtPiece
            {
                Title = "The Starry Night",
                Artist = "Vincent van Gogh"
            }));
        }

        [Test]
        public void AddArtPiece_ShouldThrowException_WhenInvalidYearIsProvided()
        {

            var futureArtPiece = new ArtPiece { Title = "Future Art", Artist = "New Artist", Year = DateTime.Now.Year + 1 };

            Assert.Throws<ArgumentOutOfRangeException>(() => _artPieceService.AddArtPiece(futureArtPiece));
        }

        [Test]
        public void AddArtPiece_ShouldThrowException_WhenTitleExceedsMaxLength()
        {

            var longTitleArtPiece = new ArtPiece
            {
                Title = new string('A', 101), // Assuming max length is 100 characters
                Artist = "Famous Artist"
            };

            Assert.Throws<ArgumentException>(() => _artPieceService.AddArtPiece(longTitleArtPiece));
        }

    }
}