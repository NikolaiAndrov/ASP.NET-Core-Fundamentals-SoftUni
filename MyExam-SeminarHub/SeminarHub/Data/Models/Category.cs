namespace SeminarHub.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidationConstants.CategoryValidation;

	public class Category
	{
        public Category()
        {
            this.Seminars = new HashSet<Seminar>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Seminar> Seminars { get; set; }
	}
}
