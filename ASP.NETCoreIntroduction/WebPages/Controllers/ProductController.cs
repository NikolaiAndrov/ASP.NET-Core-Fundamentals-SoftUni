namespace WebPages.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Net.Http.Headers;
	using System.Text;
	using System.Text.Json;
	using WebPages.Models.Product;
	using WebPages.Seeding;

	public class ProductController : Controller
	{
		ICollection<ProductViewModel> products = ProductsData.Products;

		[ActionName("My-Products")]
		public IActionResult All(string keyword)
		{
			if (keyword != null)
			{
				ICollection<ProductViewModel> productsFound = products
					.Where(p => p.Name
						.ToLower()
						.Contains(keyword.ToLower()))
					.ToHashSet();

				return View(productsFound);
			}

			return View(products);
		}

		public IActionResult ById(int id)
		{
			ProductViewModel? product = products.FirstOrDefault(x => x.Id == id);

			if (product == null)
			{
				return RedirectToAction("All");
			}

			return View(product);
		}

		public IActionResult AllAsJson()
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
			};

			return Json(products, options);
		}

		public IActionResult AllAsText()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var product in products)
			{
				sb.AppendLine(product.ToString());
			}

			return Content(sb.ToString().TrimEnd());
		}

		public IActionResult AllAsTextFile()
		{
			StringBuilder sb = new StringBuilder();

			foreach(var product in products)
			{
				sb.AppendLine(product.ToString());
			}

			Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

			return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
		}
	}
}
