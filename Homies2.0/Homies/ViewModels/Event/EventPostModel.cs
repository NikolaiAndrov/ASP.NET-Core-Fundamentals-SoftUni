namespace Homies.ViewModels.Event
{
    using Homies.ViewModels.EventType;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.EventValidation;
    using static Common.GeneralApplicationMessages;

    public class EventPostModel
    {
        public EventPostModel()
        {
            this.Types = new HashSet<EventTypeViewModel>();
        }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(DateTimeValidation, ErrorMessage = DateTimeFormatMessage)]
        public string Start { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(DateTimeValidation, ErrorMessage = DateTimeFormatMessage)]
        public string End { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        public IEnumerable<EventTypeViewModel> Types { get; set; }
    }
}
