namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     An object extension method that query if '@this' is valid number.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if valid number, false if not.</returns>
        public static bool IsValidNumber(this object @this)
        {
            if (@this == null)
            {
                return true;
            }

            return @this is sbyte
                || @this is byte
                || @this is short
                || @this is ushort
                || @this is int
                || @this is uint
                || @this is long
                || @this is ulong
                || @this is float
                || @this is double
                || @this is decimal;
        }
    }
}