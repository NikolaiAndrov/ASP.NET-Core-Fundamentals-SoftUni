namespace ForumViewModels.Post
{
    using System.ComponentModel.DataAnnotations;
    using static ForumCommon.Validations.PostValidation;

    public class PostViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
