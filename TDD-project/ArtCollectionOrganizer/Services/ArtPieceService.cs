using ArtCollectionOrganizer.Models;
using System.Collections.Generic;

namespace ArtCollectionOrganizer.Services
{
    public class ArtPieceService
    {
        public bool AddArtPiece(ArtPiece artPiece)
        {
            return false;
        }

        public bool UpdateArtPiece(ArtPiece artPiece)
        {
            return false;
        }

        public bool DeleteArtPiece(int id)
        {
            return false;
        }

        public ArtPiece GetArtPieceByTitle(string title)
        {
            return null;
        }

        public ArtPiece GetArtPieceById(int id)
        {
            return null;
        }

        public List<ArtPiece> GetAllArtPieces()
        {
            return new List<ArtPiece>();
        }

        public List<ArtPiece> SearchArtPieces(string title = null, int? yearStart = null, int? yearEnd = null)
        {
            throw new NotImplementedException();
        }

        public List<ArtPiece> FilterArtPieces(string category)
        {
            throw new NotImplementedException();
        }
    }
}
