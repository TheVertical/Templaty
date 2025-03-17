namespace Templaty.Abstractions;

public static class Template
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SourceAttribute : Attribute
    {
        public string Path { get; }

        public StoreType StoreType { get; }

        public string StoreName { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path">unique path to template content</param>
        /// <param name="storeType">template content store</param>
        public SourceAttribute(string path, StoreType storeType = StoreType.Localizations, string storeName = "default")
        {
            Path = path;
            StoreType = storeType;
            StoreName = storeName;
        }
    }

    /// <summary>
    /// describes where template stores
    /// </summary>
    public enum StoreType : byte
    {
        /// <summary>
        /// stores nowhere
        /// </summary>
        /// <remarks>used to mark an unhandled behavior</remarks>
        Unknown = 0,

        /// <summary>
        /// stores arround localizations
        /// </summary>
        Localizations = 1,

        /// <summary>
        /// stores arround embedded resources
        /// </summary>
        Resources = 2,

        /// <summary>
        /// stores by not defined method
        /// </summary>
        /// <remarks>use it for custom stores</remarks>
        NotDefined = 3
    }
}