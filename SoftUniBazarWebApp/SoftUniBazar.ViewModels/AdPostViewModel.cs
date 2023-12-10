namespace SoftUniBazar.ViewModels
{
	using System.ComponentModel.DataAnnotations;
	using static Common.EntityValidations.AdValidation;

	public class AdPostViewModel
	{
        public AdPostViewModel()
        {
            this.Categories = new HashSet<CategorySelectViewModel>();
        }

		[Required(AllowEmptyStrings = false)]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = PriceErrorMessage)]
		public decimal Price { get; set; }

		[Required]
		public int CategoryId { get; set; }

		public ICollection<CategorySelectViewModel> Categories { get; set; }
	}
}
