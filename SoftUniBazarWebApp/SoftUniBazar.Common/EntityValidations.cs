namespace SoftUniBazar.Common
{
	public static class EntityValidations
	{
		public static class AdValidation
		{
			public const int NameMinLength = 5;
			public const int NameMaxLength = 25;

			public const int DescriptionMinLength = 15;
			public const int DescriptionMaxLength = 250;

			public const string PriceMinValue = "0.01";
			public const string PriceMaxValue = "100000000";
			public const string PriceErrorMessage = "The price should be between 0.01 and 100000000";
		}

		public static class CategoryValidation
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 15;
		}
	}
}
