namespace SoftUniBazar.ViewModels
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidations.CategoryValidation;

	public class CategorySelectViewModel
	{
		[Required]
		[Range(1, int.MaxValue)]
		public int Id { get; set; }

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
