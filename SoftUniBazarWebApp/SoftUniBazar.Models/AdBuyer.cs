using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Models
{
	public class AdBuyer
	{
		public string BuyerId { get; set; } = null!;

		[ForeignKey(nameof(BuyerId))]
		public virtual IdentityUser Buyer { get; set; } = null!;

		public int AdId { get; set; }

		[ForeignKey(nameof(AdId))]	
		public virtual Ad Ad { get; set; } = null!;
	}
}
