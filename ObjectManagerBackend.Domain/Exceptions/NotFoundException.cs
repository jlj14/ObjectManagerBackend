using System.Globalization;

namespace ObjectManagerBackend.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for not found
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
