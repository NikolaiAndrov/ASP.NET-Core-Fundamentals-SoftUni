namespace SeminarHub.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Microsoft.AspNetCore.Identity;

	public class SeminarParticipant
	{
		[Required]
		public int SeminarId { get; set; }

		[ForeignKey(nameof(SeminarId))]
		public virtual Seminar Seminar { get; set; } = null!;

		[Required]
		public string ParticipantId { get; set; } = null!;

		[ForeignKey(nameof(ParticipantId))]
		public virtual IdentityUser Participant { get; set; } = null!;
	}
}
