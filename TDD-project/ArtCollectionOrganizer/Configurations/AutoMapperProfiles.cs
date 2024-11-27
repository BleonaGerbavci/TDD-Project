using ArtCollectionOrganizer.DTOs;
using ArtCollectionOrganizer.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArtCollectionOrganizer.Configurations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArtPiece, ArtPieceDto>().ReverseMap();
        }
    }
}
