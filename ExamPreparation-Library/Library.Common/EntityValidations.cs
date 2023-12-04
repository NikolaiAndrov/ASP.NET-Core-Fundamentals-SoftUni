namespace Library.Common
{
	public static class EntityValidations
	{
		public static class BookValidation
		{
			public const int TitleMinLength = 10;
			public const int TitleMaxLength = 50;

			public const int AuthorMinLength = 5;
			public const int AuthorMaxLength = 50;

			public const int DescriptionMinLength = 5;
			public const int DescriptionMaxLength = 5000;

			public const string RatingMinValue = "0.00";
			public const string RatingMaxValue = "10.00";
		}

        public static class CategoryValidation
		{
			public const int NameMinLength = 5;
			public const int NameMaxLength = 50;
        }
    }
}
