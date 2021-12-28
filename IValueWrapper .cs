namespace hex.mvvm
{
    /// <summary>
    /// Interface of value wrapper
    /// </summary>
    public interface IValueWrapper
    {
        /// <summary>
        /// Value
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Gets the presence of a value
        /// </summary>
        bool HasValue { get; }
    }
}