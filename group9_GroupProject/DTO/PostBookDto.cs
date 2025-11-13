namespace group9_GroupProject.DTO
{
    public class PostBookDto
    {
        public string Isbn { get; set; } = null!;

        public string BookTitle { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int CategoryId { get; set; }

        public int PublisherId { get; set; }

        public int Quantity { get; set; }
    }
}
