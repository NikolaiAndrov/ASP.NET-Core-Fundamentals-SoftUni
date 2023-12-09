namespace SoftUniBazar.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static Common.EntityValidations.AdValidation;

	public class Ad
	{
        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public string OwnerId { get; set; } = null!;

		[ForeignKey(nameof(OwnerId))]
		public virtual IdentityUser Owner { get; set; } = null!;

		[Required]
		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public virtual Category Category { get; set; } = null!;
	}
}
