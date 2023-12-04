namespace Library.Controllers
{
	using Library.Services.Interfaces;
	using Library.ViewModels.Book;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class BookController : Controller
	{
		private IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> All()
		{
			ICollection<BookViewModel> books = await bookService.GetAllBooksAsync();
			return View(books);
		}
	}
}
