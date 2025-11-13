namespace group9_GroupProject.DTO
{
    public class GetBookDetailDto
    {
        public int BookId { get; set; }

        public string Isbn { get; set; } = null!;

        public string BookTitle { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string CategoryName { get; set; }

        public string PublisherName { get; set; }

        public int Quantity { get; set; }
    }
}
