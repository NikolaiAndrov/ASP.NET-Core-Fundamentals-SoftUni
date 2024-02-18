namespace SeminarHub.Models.Seminar
{
	using System.ComponentModel.DataAnnotations;

	using Category;
	using static Common.EntityValidationConstants.SeminarValidation;
	using static Common.EntityValidationConstants.CategoryValidation;

	public class SeminarPostModel
	{
        public SeminarPostModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        [Required(AllowEmptyStrings = false)]
		[StringLength(TopicMaxLength, MinimumLength = TopicMinLength)]
		public string Topic { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		[StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength)]
		public string Lecturer { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		[StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
		public string Details { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		public string DateAndTime { get; set; } = null!;

		[Required]
		[Range(DurationMinValue, DurationMaxValue)]
		public int Duration { get; set; }

		[Required]
		[Range(CategoryKeyMinValue, CategoryKeyMaxValue)]
		public int CategoryId { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; }
	}
}
