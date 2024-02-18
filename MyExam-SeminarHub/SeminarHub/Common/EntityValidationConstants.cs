namespace SeminarHub.Common
{
	public static class EntityValidationConstants
	{
		public static class SeminarValidation
		{
			public const int TopicMinLength = 3;
			public const int TopicMaxLength = 100;

			public const int LecturerMinLength = 5;
			public const int LecturerMaxLength = 60;

			public const int DetailsMinLength = 10;
			public const int DetailsMaxLength = 500;

			public const int DurationMinValue = 30;
			public const int DurationMaxValue = 180;
		}

		public static class CategoryValidation
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 50;

			public const int CategoryKeyMinValue = 1;
			public const int CategoryKeyMaxValue = int.MaxValue;
		}
	}
}
