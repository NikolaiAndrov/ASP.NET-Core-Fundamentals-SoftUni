namespace Homies.Data.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations.Schema;

	public class EventParticipant
	{
		public string HelperId { get; set; } = null!;

		[ForeignKey(nameof(HelperId))]
		public virtual IdentityUser Helper { get; set; } = null!;

		public int EventId { get; set; }

		[ForeignKey(nameof(EventId))]
		public virtual Event Event { get; set; } = null!;
	}
}
