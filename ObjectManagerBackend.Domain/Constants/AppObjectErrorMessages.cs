namespace ObjectManagerBackend.Domain.Constants
{
    /// <summary>
    /// Class to store the error messages for the AppObject entity
    /// </summary>
    public static class AppObjectErrorMessages
    {
        public const string OBJECT_DELETE_CASCADE_NOT_ALLOWED = "Cascade delete is not allowed; clean the chils objects first";
        public const string OBJECT_NOT_FOUND = "Object not found";
        public const string PARENT_OBJECT_NOT_FOUND = "Parent object not found";
        public const string INVALID_OBJECT_ID = "Object id shoul be grater than 0";
        public const string OBJECT_NAME_IS_REQUIRED = "Object name is required";
        public const string OBJECT_TYPE_IS_REQUIRED = "Object type is required";
        public const string OBJECT_DESCRIPTION_IS_REQUIRED = "Object description is required";
    }
}
