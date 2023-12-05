namespace Library.ViewModels.Book
{
	using Library.ViewModels.Category;
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidations.BookValidation;

	public class BookPostModel
	{
        public BookPostModel()
        {
            this.Categories = new HashSet<CategoryPostModel>();
        }

		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

		[Required]
		[StringLength(AuthorMaxLength, MinimumLength = AuthorMinLength)]
		public string Author { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(typeof(decimal), RatingMinValue, RatingMaxValue)]
		public decimal Rating { get; set; }

		[Required]
		public int CategoryId { get; set; }

		public ICollection<CategoryPostModel> Categories { get; set; }
	}
}
