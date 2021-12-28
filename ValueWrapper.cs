using hex.mvvm;

/// <summary>
/// Type of value wrapper
/// </summary>
/// <typeparam name="T">Type</typeparam>
public class ValueWrapper<T> : IValueWrapper
{
    /// <summary>
    /// Value
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Value
    /// </summary>
    object IValueWrapper.Value
    {
        get { return Value; }
        set { this.Value = (T)value; }
    }

    /// <summary>
    /// Gets the presence of a value
    /// </summary>
    public bool HasValue { get => this.Value != null; }

    /// <summary>
    /// ctor
    /// </summary>
    public ValueWrapper() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value">Value</param>
    public ValueWrapper(T value)
    {
        this.Value = value;
    }
}