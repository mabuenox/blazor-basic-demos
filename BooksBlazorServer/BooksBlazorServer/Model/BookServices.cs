using Bogus;

namespace BooksBlazorServer.Model
{
    public class BookServices
    {
        private static readonly List<Book> Books;

        static BookServices()
        {
            Books = new List<Book>();

            var faker = new Faker<Book>("en")
              .RuleFor(u => u.Title, (f, u) => f.Lorem.Sentence())
              .RuleFor(u => u.Author, (f, u) => f.Name.FullName())
              .RuleFor(u => u.Price, (f, u) => f.Random.Decimal(0.15m, 0.99m) * 100)
              .RuleFor(u => u.ImageUrl, (f, u) => f.Image.PicsumUrl())
              .RuleFor(u => u.ReleaseDate, (f, u) => f.Date.Past(30));

            var seed = faker.GenerateForever();

            for (int i = 0; i <= 100; i++)
            {
                var book = seed.ElementAt(i);
                book.Id = i + 1;
                Books.Add(book);
            }
        }

        public IReadOnlyList<Book> GetBooks(int page, string orderBy = "id", string orderDirection = "asc")
        {
            Func<Book, object> order = orderBy switch
            {
                "title" => f => f.Title,
                "author" => f => f.Author,
                "price" => f => f.Price,
                "release_date" => f => f.ReleaseDate,
                "likes" => f => f.Likes,
                _ => f => f.Id
            };

            IEnumerable<Book> books;

            if (orderDirection == "desc")
            {
                books = Books.OrderByDescending(order);
            }
            else
            {
                books = Books.OrderBy(order);
            }

            books = books.Skip(10 * (page - 1)).Take(10);
            return books.ToList();
        }

        public int GetTotalPages()
        {
            return (int)Math.Ceiling((double)Books.Count / 10);
        }
    }
}
