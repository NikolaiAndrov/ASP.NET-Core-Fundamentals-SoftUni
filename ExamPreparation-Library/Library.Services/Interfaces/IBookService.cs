namespace Library.Services.Interfaces
{
	using Library.ViewModels.Book;

	public interface IBookService
	{
		Task<ICollection<BookViewModel>> GetAllBooksAsync();

		Task AddToCollectionAsync(int bookId, string collectorId);

		Task<ICollection<BookMineViewModel>> GetMineBooksAsync(string userId);
	}
}
