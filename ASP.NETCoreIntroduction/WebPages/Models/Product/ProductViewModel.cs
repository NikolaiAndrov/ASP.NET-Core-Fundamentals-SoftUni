namespace WebPages.Models.Product
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public override string ToString()
		{
			return $"Product {this.Id}: {this.Name} - {this.Price} lv.";
		}
	}
}
