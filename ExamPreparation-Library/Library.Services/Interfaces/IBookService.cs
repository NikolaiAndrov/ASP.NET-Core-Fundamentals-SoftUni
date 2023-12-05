namespace Library.Services.Interfaces
{
	using Library.ViewModels.Book;
	using Library.ViewModels.Category;

	public interface IBookService
	{
		Task<ICollection<BookViewModel>> GetAllBooksAsync();

		Task AddToCollectionAsync(int bookId, string collectorId);

		Task<ICollection<BookMineViewModel>> GetMineBooksAsync(string userId);

		Task RemoveFromMineCollection(int bookId, string userId);

		Task<ICollection<CategoryPostModel>> GetAllCategories();

		Task AddAsync(BookPostModel model);
	}
}
