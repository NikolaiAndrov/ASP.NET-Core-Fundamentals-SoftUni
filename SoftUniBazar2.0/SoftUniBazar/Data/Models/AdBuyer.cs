namespace SoftUniBazar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AdBuyer
    {
        [Required]
        public string BuyerId { get; set; } = null!;

        [ForeignKey(nameof(BuyerId))]
        public virtual IdentityUser Buyer { get; set; } = null!;

        [Required]
        public int AdId { get; set; }

        [ForeignKey(nameof(AdId))]
        public virtual Ad Ad { get; set; } = null!;
    }
}
