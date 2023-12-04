namespace Library.Services.Interfaces
{
	using Library.ViewModels.Book;

	public interface IBookService
	{
		Task<ICollection<BookViewModel>> GetAllBooksAsync();
	}
}
