using BooksBlazorServer.Model;

namespace BooksBlazorServer.Pages
{
    public partial class Books
    {
        int currentPage = 1;
        string sortBy = null;
        string sortDirection = "asc";

        private static BookServices bookServices = new BookServices();
        private IEnumerable<Book> books = bookServices.GetBooks(1);

        private void NextPage()
        {
            currentPage++;
            Reload();
        }
        private void PrevPage()
        {
            currentPage--;
            Reload();
        }

        private void SortBy(string field)
        {
            if (sortBy == field)
            {
                sortDirection = sortDirection == "asc" ? "desc" : "asc";
            }
            sortBy = field;
            currentPage = 1;
            Reload();
        }

        private void Reload()
        {
            books = bookServices.GetBooks(currentPage, sortBy, sortDirection);
        }
    }
}
