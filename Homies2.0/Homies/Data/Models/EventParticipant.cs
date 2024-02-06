namespace Homies.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventParticipant
    {
        [Required]
        public string HelperId { get; set; } = null!;

        [ForeignKey(nameof(HelperId))]
        public virtual IdentityUser Helper { get; set; } = null!;

        [Required]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; } = null!;
    }
}
