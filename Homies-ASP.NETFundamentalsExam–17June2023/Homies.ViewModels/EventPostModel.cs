namespace Homies.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidations.EventValidation;

	public class EventPostModel
	{
        public EventPostModel()
        {
            this.Types = new HashSet<TypePostModel>();
        }

        [Required(AllowEmptyStrings = false)]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		public string Start {  get; set; } = null!;

		[Required(AllowEmptyStrings = false)]
		public string End { get; set; } = null!;

		public int TypeId { get; set; }

		public ICollection<TypePostModel> Types { get; set; }
	}
}
