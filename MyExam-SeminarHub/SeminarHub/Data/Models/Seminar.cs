namespace SeminarHub.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Microsoft.AspNetCore.Identity;

	using static Common.EntityValidationConstants.SeminarValidation;

	public class Seminar
	{
        public Seminar()
        {
            this.SeminarsParticipants = new HashSet<SeminarParticipant>();
        }

        [Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(TopicMaxLength)]
		public string Topic { get; set; } = null!;

		[Required]
		[MaxLength(LecturerMaxLength)]
		public string Lecturer { get; set; } = null!;

		[Required]
		[MaxLength(DetailsMaxLength)]
		public string Details { get; set; } = null!;

		[Required]
		public string OrganizerId { get; set; } = null!;

		[ForeignKey(nameof(OrganizerId))]
		public virtual IdentityUser Organizer { get; set; } = null!;

		[Required]
		public DateTime DateAndTime { get; set; }

		[Required]
		public int Duration { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public virtual Category Category { get; set; } = null!;

		public virtual ICollection<SeminarParticipant> SeminarsParticipants { get; set; }
	}
}
