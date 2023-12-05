namespace Library.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Common.EntityValidations.CategoryValidation;

    public class CategoryPostModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
