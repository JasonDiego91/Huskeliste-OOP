namespace Huskeliste
{
    internal class SharedData
    {
        public string? Title { get; set; }
        public string? Creator { get; set; }
        public string? Description { get; set; }
        public DateTime Length { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        public string GetReleaseDate()
        {
            return ReleaseDate.ToString("D");
        }

    }
}
