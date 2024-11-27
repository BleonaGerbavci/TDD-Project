using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtCollectionOrganizer.Interfaces
{
    public interface IArtPieceService
    {
        public Task<ActionResult<List<ArtPieceDto>>> GetAllArtPieces();
        public Task<ActionResult<ArtPieceDto>> GetArtPieceById(int id);
        public Task<ActionResult<ArtPieceDto>> GetArtPieceByTitle(string title);
        public Task<ActionResult> AddArtPiece(ArtPieceDto artPieceDto);
        public Task<ActionResult> UpdateArtPiece(int id, UpdateArtPieceDto updateArtPieceDto);
        public Task<ActionResult> DeleteArtPiece(int id);
        public Task<List<ArtPiece>> SearchArtPieces(string title = null, int? yearStart = null, int? yearEnd = null);
        public Task<List<ArtPiece>> FilterArtPieces(string category);
    }
}
