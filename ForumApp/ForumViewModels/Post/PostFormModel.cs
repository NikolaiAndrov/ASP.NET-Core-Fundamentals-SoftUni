namespace ForumViewModels.Post
{
	using System.ComponentModel.DataAnnotations;
	using static ForumCommon.Validations.PostValidation;
	using static ForumCommon.Validations.PostErrorMessages;

	public class PostFormModel
	{
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = PostTitleLengthRange)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = PostContentLengthRange)]
		public string Content { get; set; } = null!;
	}
}
