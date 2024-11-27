using ArtCollectionOrganizer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ArtCollectionOrganizer.Configurations
{
    public class ArtDBContext : DbContext
    {
        public ArtDBContext(DbContextOptions<ArtDBContext> options) : base(options) { }

        public DbSet<ArtPiece> ArtPieces { get; set; }
       
    }
}
