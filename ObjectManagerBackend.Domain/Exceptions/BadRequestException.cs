using System.Globalization;

namespace ObjectManagerBackend.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for bad requests
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
