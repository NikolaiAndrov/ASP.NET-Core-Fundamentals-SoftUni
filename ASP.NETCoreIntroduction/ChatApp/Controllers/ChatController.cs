namespace ChatApp.Controllers
{
	using ChatApp.Models.Message;
	using Microsoft.AspNetCore.Mvc;

	public class ChatController : Controller
	{
		private static ICollection<KeyValuePair<string, string>> messages =
			new List<KeyValuePair<string, string>>();

		public IActionResult Show()
		{
			if (messages.Count < 1)
			{
				return View(new ChatViewModel());
			}

			var chatModel = new ChatViewModel()
			{
				Messages = messages
				.Select(m => new MessageViewModel()
				{
					Sender = m.Key,
					MessageText = m.Value
				})
				.ToList()
			};

			return View(chatModel);
		}

		[HttpPost]
		public IActionResult Send(ChatViewModel chat)
		{
			var newMessages = chat.CurrentMessage;

			messages.Add(new KeyValuePair<string, string>(newMessages.Sender, newMessages.MessageText));

			return RedirectToAction("Show");
		}
	}
}
