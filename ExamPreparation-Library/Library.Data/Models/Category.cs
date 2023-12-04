namespace Library.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidations.CategoryValidation;

	public class Category
	{
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Book> Books { get; set; }
	}
}
