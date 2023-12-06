namespace Homies.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidations.TypeValidation;

	public class TypePostModel
	{
		[Range(1, int.MaxValue)]
		public int Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;
	}
}
