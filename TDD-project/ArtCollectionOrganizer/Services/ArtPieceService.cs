using ArtCollectionOrganizer.Configurations;
using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Interfaces;
using ArtCollectionOrganizer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ArtCollectionOrganizer.Services
{
    public class ArtPieceService : IArtPieceService
    {
        private readonly ArtDBContext _context;
        private readonly IMapper _mapper;

        public ArtPieceService(ArtDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<ArtPieceDto>>> GetAllArtPieces() =>
            _mapper.Map<List<ArtPieceDto>>(await _context.ArtPieces.ToListAsync());

        public async Task<ActionResult<ArtPieceDto>> GetArtPieceById(int id)
        {
            var artPiece = _mapper.Map<ArtPieceDto>(await _context.ArtPieces.FindAsync(id));
            return artPiece == null
                ? new NotFoundObjectResult("Art piece does not exist!")
                : new OkObjectResult(artPiece);
        }
        public async Task<ActionResult> AddArtPiece(ArtPieceDto artPieceDto)
        {
            if (artPieceDto == null)
                return new BadRequestObjectResult("Art piece cannot be null!");

            if (string.IsNullOrWhiteSpace(artPieceDto.Title) ||
                string.IsNullOrWhiteSpace(artPieceDto.Artist) ||
                artPieceDto.Year <= 0 ||
                string.IsNullOrWhiteSpace(artPieceDto.Category) ||
                string.IsNullOrWhiteSpace(artPieceDto.Material) ||
                string.IsNullOrWhiteSpace(artPieceDto.Size) ||
                artPieceDto.Price < 0)
            {
                return new BadRequestObjectResult("All required fields must be provided.");
            }

            var existingArtPiece = await _context.ArtPieces
                .FirstOrDefaultAsync(ap => ap.Title == artPieceDto.Title && ap.Artist == artPieceDto.Artist);

            if (existingArtPiece != null)
            {
                return new BadRequestObjectResult("An art piece with the same title and artist already exists.");
            }

            if (artPieceDto.Title.Length > 100)
            {
                return new BadRequestObjectResult("The title exceeds the maximum allowed length of 100 characters.");
            }

            if (artPieceDto.Year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(artPieceDto.Year), "The year cannot be in the future.");
            }

            var artPiece = _mapper.Map<ArtPiece>(artPieceDto);
            await _context.ArtPieces.AddAsync(artPiece);
            await _context.SaveChangesAsync();
            return new OkObjectResult(artPiece);
        }



        public async Task<ActionResult> UpdateArtPiece(int id, UpdateArtPieceDto updateArtPieceDto)
        {
            if (updateArtPieceDto == null)
                return new BadRequestObjectResult("Art piece cannot be null!");

            var artPiece = await _context.ArtPieces.FindAsync(id);
            if (artPiece == null)
                return new BadRequestObjectResult("Art piece does not exist!");

            if (updateArtPieceDto.Year.HasValue && updateArtPieceDto.Year.Value > DateTime.Now.Year)
            {
                return new BadRequestObjectResult("Year cannot be in the future.");
            }

            if (updateArtPieceDto.Price.HasValue && updateArtPieceDto.Price.Value < 0)
            {
                return new BadRequestObjectResult("Price cannot be negative.");
            }

            artPiece.Title = updateArtPieceDto.Title ?? artPiece.Title;
            artPiece.Artist = updateArtPieceDto.Artist ?? artPiece.Artist;
            artPiece.Year = updateArtPieceDto.Year ?? artPiece.Year;
            artPiece.Category = updateArtPieceDto.Category ?? artPiece.Category;
            artPiece.Material = updateArtPieceDto.Material ?? artPiece.Material;
            artPiece.IsSold = updateArtPieceDto.IsSold ?? artPiece.IsSold;
            artPiece.Size = updateArtPieceDto.Size ?? artPiece.Size;
            artPiece.Price = updateArtPieceDto.Price ?? artPiece.Price;

            await _context.SaveChangesAsync();
            return new OkObjectResult("Art piece was successfully updated!");
        }

        public async Task<ActionResult> DeleteArtPiece(int id)
        {
            var artPiece = await _context.ArtPieces.FindAsync(id);
            if (artPiece == null)
                return new BadRequestObjectResult("Art piece does not exist!");

            _context.ArtPieces.Remove(artPiece);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Art piece was successfully deleted!");
        }


        public async Task<List<ArtPiece>> SearchArtPieces(string title = null, int? yearStart = null, int? yearEnd = null)
        {
            var query = _context.ArtPieces.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(a => a.Title.ToLower().Contains(title.ToLower()));
            }

            if (yearStart.HasValue)
            {
                query = query.Where(a => a.Year >= yearStart.Value);
            }

            if (yearEnd.HasValue)
            {
                query = query.Where(a => a.Year <= yearEnd.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<List<ArtPiece>> FilterArtPieces(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentException("Category cannot be null or empty!", nameof(category));
            }

            return await _context.ArtPieces
                .Where(a => a.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }

        public async Task<ActionResult> BuyArtPiece(int id)
        {
            var artPiece = await _context.ArtPieces.FindAsync(id);
            if (artPiece == null)
                return new NotFoundObjectResult("Art piece does not exist!");

            if (artPiece.IsSold)
                return new BadRequestObjectResult("Art piece is already sold!");

            artPiece.IsSold = true;

            await _context.SaveChangesAsync();

            return new OkObjectResult($"Art piece '{artPiece.Title}' by {artPiece.Artist} was successfully purchased!");
        }

    }
}
