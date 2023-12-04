namespace Library.Services
{
	using Library.Data;
	using Library.Services.Interfaces;
	using Library.ViewModels.Book;
	using Microsoft.EntityFrameworkCore;

	public class BookService : IBookService
	{
		private readonly LibraryDbContext context;

        public BookService(LibraryDbContext context)
        {
            this.context = context;	
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
	}
}
