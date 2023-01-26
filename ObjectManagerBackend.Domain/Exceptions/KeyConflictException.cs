using System.Globalization;

namespace ObjectManagerBackend.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for key conflicts
    /// </summary>
    public class KeyConflictException : Exception
    {
        public KeyConflictException() : base() { }

        public KeyConflictException(string message) : base(message) { }

        public KeyConflictException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
