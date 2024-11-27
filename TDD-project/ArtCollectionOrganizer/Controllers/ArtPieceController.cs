using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtCollectionOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtPieceController : ControllerBase
    {
        private readonly IArtPieceService _artPieceService;

        public ArtPieceController(IArtPieceService artPieceService)
        {
            _artPieceService = artPieceService;
        }

        [HttpGet("artpieces")]
        public async Task<ActionResult<List<ArtPieceDto>>> GetArtPieces()
        {
            return await _artPieceService.GetAllArtPieces();
        }

        [HttpGet("artpiece")]
        public async Task<ActionResult<ArtPieceDto>> GetArtPieceById(int id)
        {
            return await _artPieceService.GetArtPieceById(id);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ArtPieceDto>>> SearchArtPieces(
        [FromQuery] string title = null,
        [FromQuery] int? yearStart = null,
        [FromQuery] int? yearEnd = null)
        {
            var result = await _artPieceService.SearchArtPieces(title, yearStart, yearEnd);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<ArtPieceDto>>> FilterArtPieces([FromQuery] string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category parameter is required.");
            }

            var result = await _artPieceService.FilterArtPieces(category);
            return Ok(result);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<ArtPieceDto>> GetArtPieceByTitle(string title)
        {
            var result = await _artPieceService.GetArtPieceByTitle(title);
            if (result == null)
            {
                return NotFound($"No art piece found with the title '{title}'");
            }
            return Ok(result);
        }

        [HttpPost("artpiece")]
        public async Task<ActionResult> AddArtPiece(ArtPieceDto artPieceDto)
        {
            return await _artPieceService.AddArtPiece(artPieceDto);
        }

        [HttpPut("artpiece")]
        public async Task<ActionResult> UpdateArtPiece(int id, UpdateArtPieceDto updateArtPieceDto)
        {
            return await _artPieceService.UpdateArtPiece(id, updateArtPieceDto);
        }

        [HttpDelete("artpiece")]
        public async Task<ActionResult> DeleteArtPiece(int id)
        {
            return await _artPieceService.DeleteArtPiece(id);
        }
    }
}
