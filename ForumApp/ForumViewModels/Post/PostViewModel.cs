namespace ForumViewModels.Post
{
    using System.ComponentModel.DataAnnotations;
    using static ForumCommon.Validations.PostValidation;

    public class PostViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
