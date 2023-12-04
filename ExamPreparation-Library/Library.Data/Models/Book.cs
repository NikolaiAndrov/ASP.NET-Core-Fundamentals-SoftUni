namespace Library.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static Common.EntityValidations.BookValidation;

	public class Book
	{
        public Book()
        {
            this.UsersBooks = new HashSet<IdentityUserBook>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(AuthorMaxLength)]
		public string Author { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public decimal Rating { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		[ForeignKey(nameof(CategoryId))]
		public virtual Category Category { get; set; } = null!;

		public virtual ICollection<IdentityUserBook> UsersBooks { get;}
	}
}
