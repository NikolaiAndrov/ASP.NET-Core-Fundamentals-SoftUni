namespace Homies.Common
{
    public static class EntityValidationConstants
    {
        public static class EventValidation
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 20;

            public const int DescriptionMinLength = 15;
            public const int DescriptionMaxLength = 150;

            public const string DateTimeValidation = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}";
        }

        public static class TypeValidation
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 15;
        }
    }
}
