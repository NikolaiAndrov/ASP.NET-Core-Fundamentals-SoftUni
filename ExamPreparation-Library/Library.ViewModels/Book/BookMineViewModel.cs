namespace Library.ViewModels.Book
{
	public class BookMineViewModel
	{
		public int Id { get; set; }

		public string ImageUrl { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Author { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;
	}
}
