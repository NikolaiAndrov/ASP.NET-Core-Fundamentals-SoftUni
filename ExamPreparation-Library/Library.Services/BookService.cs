namespace Library.Services
{
	using Library.Data;
	using Library.Data.Models;
	using Library.Services.Interfaces;
	using Library.ViewModels.Book;
	using Library.ViewModels.Category;
	using Microsoft.EntityFrameworkCore;

	public class BookService : IBookService
	{
		private readonly LibraryDbContext context;

        public BookService(LibraryDbContext context)
        {
            this.context = context;	
        }

		public async Task AddAsync(BookPostModel model)
		{
			if (string.IsNullOrWhiteSpace(model.Author) || 
				string.IsNullOrWhiteSpace(model.Description) || 
				string.IsNullOrWhiteSpace(model.ImageUrl) ||
				string.IsNullOrWhiteSpace(model.Title))
			{
				throw new InvalidOperationException();
			}

		    Book book = new Book
			{
				Title = model.Title,
				Author = model.Author,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				Rating = model.Rating,
				CategoryId = model.CategoryId,
			};

			await context.Books.AddAsync(book);
			await context.SaveChangesAsync();
		}

		public async Task AddToCollectionAsync(int bookId, string collectorId)
		{
			if (context.UsersBooks.Any(ub => ub.BookId == bookId && ub.CollectorId == collectorId))
			{
				throw new InvalidOperationException();
			}

			IdentityUserBook userBook = new IdentityUserBook
			{
				CollectorId = collectorId,
				BookId = bookId
			};

		 	await context.UsersBooks.AddAsync(userBook);
			await context.SaveChangesAsync();
		}

		public async Task<ICollection<BookViewModel>> GetAllBooksAsync()
		{
			ICollection<BookViewModel> books = await context.Books
				.Select(b => new BookViewModel
				{	
					Id = b.Id,
					ImageUrl = b.ImageUrl,
					Title = b.Title,
					Author = b.Author,
					Rating = b.Rating,
					Category = b.Category.Name
				})
				.ToListAsync();

			return books;
		}

		public async Task<ICollection<CategoryPostModel>> GetAllCategories()
		{
			ICollection<CategoryPostModel> categories = await context.Categories
				.Select(c => new CategoryPostModel
				{
					CategoryId = c.Id,
					Name = c.Name,
				})
				.ToListAsync();

			return categories;
		}

		public async Task<ICollection<BookMineViewModel>> GetMineBooksAsync(string userId)
		{
			ICollection<BookMineViewModel> books = await context.UsersBooks
				.Where(ub => ub.CollectorId == userId)
				.Select(b => new BookMineViewModel
				{
					Id = b.Book.Id,
					ImageUrl = b.Book.ImageUrl,
					Title= b.Book.Title,
					Author = b.Book.Author,
					Description = b.Book.Description,
					Category = b.Book.Category.Name
				})
				.ToListAsync();

			return books;
		}

		public async Task RemoveFromMineCollection(int bookId, string userId)
		{
			IdentityUserBook userBook = await context.UsersBooks
				.FirstAsync(ub => ub.CollectorId == userId && ub.BookId == bookId);

			context.UsersBooks.Remove(userBook);
			await context.SaveChangesAsync();
		}
	}
}
