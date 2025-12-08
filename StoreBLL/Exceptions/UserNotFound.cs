namespace StoreBLL.Exceptions;

/// <summary>
///  A custom exception class that represents a not logged-in user
/// </summary>
public class UserNotFound : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotFound"/> class.
    /// </summary>
    public UserNotFound()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotFound"/> class.
    /// </summary>
    /// <param name="message">A message of exception.</param>
    public UserNotFound(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotFound"/> class.
    /// </summary>
    /// <param name="message">A message of exception.</param>
    /// <param name="innerException">An inner exception.</param>
    public UserNotFound(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}