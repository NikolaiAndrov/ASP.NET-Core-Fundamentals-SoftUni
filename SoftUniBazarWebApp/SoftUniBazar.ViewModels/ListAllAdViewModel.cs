namespace SoftUniBazar.ViewModels
{
	public class ListAllAdViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public string CreatedOn { get; set; } = null!;

		public string Category {  get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Price { get; set; } = null!;

		public string Owner { get; set; } = null!;
	}
}
