namespace Homies.Data.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using static Common.EntityValidations.EventValidation;

	public class Event
	{
        public Event()
        {
            this.EventsParticipants = new HashSet<EventParticipant>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set;}

		[Required]
		public DateTime End { get; set;}

		[Required]
		public string OrganiserId { get; set; } = null!;

		[ForeignKey(nameof(OrganiserId))]
		public virtual IdentityUser Organiser { get; set; } = null!;

		[Required]
		public int TypeId { get; set; }

		[ForeignKey(nameof(TypeId))]
		public virtual Type Type { get; set; } = null!;

		public virtual ICollection<EventParticipant> EventsParticipants { get; set; }
	}
}
