namespace ArtCollectionOrganizer.DTOs
{
    public class ArtPieceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string Material { get; set; }
        public bool IsSold { get; set; }
        public string Size { get; set; }
        public Double Price { get; set; }

    }

    public class UpdateArtPieceDto
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public int? Year { get; set; }
        public string? Category { get; set; }
        public string? Material { get; set; }
        public bool? IsSold { get; set; }
        public string? Size { get; set; }
        public Double? Price { get; set; }

    }
}
