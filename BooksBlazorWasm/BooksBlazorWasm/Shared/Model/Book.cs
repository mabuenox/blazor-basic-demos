namespace BooksBlazorWasm.Shared.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Likes { get; set; }

        public void Like()
        {
            Likes++;
        }

    }
}
