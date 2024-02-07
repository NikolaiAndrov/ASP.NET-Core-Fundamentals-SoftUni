namespace SoftUniBazar.ViewModels.Ad
{
    using SoftUniBazar.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.AdValidation;

    public class AdPostModel
    {
        public AdPostModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal  Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
