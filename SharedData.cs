namespace Huskeliste
{
    internal class SharedData
    {
        public string? Title { get; set; }
        public string? Creator { get; set; }
        public string? Description { get; set; }
        public DateTime Length { get; set; }
        public DateTime Getdate { get; set; } = DateTime.Today;

        public string GetDate()
        {
            return Getdate.ToString("D");
        }

    }
}
