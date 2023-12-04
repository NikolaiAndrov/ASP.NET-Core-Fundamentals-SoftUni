namespace Library.Data.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class IdentityUserBook
	{
		[Required]
		public string CollectorId { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(CollectorId))]
		public virtual IdentityUser Collector { get; set; } = null!;

		[Required]
		public int BookId { get; set; }

		[Required]
		[ForeignKey(nameof(BookId))]
		public virtual Book Book { get; set; } = null!;
	}
}
