namespace Library.Controllers
{
	using Library.Services.Interfaces;
	using Library.ViewModels.Book;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;

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

		public async Task<IActionResult> AddToCollection(int Id)
		{
			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await bookService.AddToCollectionAsync(Id, userId);
			}
			catch (Exception)
			{
			}

			return RedirectToAction("All", "Book");
		}

		public async Task<IActionResult> Mine()
		{
			ICollection<BookMineViewModel> books = new List<BookMineViewModel>();

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				books = await bookService.GetMineBooksAsync(userId);
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Book");
			}

			return View(books);
		}

		public async Task<IActionResult> RemoveFromCollection(int Id)
		{
			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await bookService.RemoveFromMineCollection(Id, userId);
			}
			catch (Exception)
			{
			}

			return RedirectToAction("Mine", "Book");
		}

		public async Task<IActionResult> Add()
		{
			BookPostModel book = new BookPostModel
			{
				Categories = await bookService.GetAllCategories()
			};

			return View(book);
		}

		[HttpPost]
		public async Task<IActionResult> Add(BookPostModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await bookService.GetAllCategories();
				return View(model);
			}

			try
			{
				await bookService.AddAsync(model);
			}
			catch (Exception)
			{
				model.Categories = await bookService.GetAllCategories();
				return View(model);
			}

			return RedirectToAction("All", "Book");
		}						
	}
}
