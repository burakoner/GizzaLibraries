using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets decimal places count
        /// </summary>
        /// <param name="this"></param>
        /// <returns>
        /// <para>Console.WriteLine(1/3m); //this is 0.3333333333333333333333333333</para>
        /// <para>Console.WriteLine(1/3f); //this is 0.3333333</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(0.0m));                  // 0</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(1/3m));                  // 28</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces((decimal)(1 / 3f)));     // 7</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(-1.123m));               // 3</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(43.12345m));             // 5</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(0));                     // 0</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(0.01m));                 // 2</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces(-0.001m));               // 3</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces((decimal)-0.00000001f)); // 8</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces((decimal)0.0001234f));   // 7</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces((decimal)0.01f));        // 2</para>
        /// <para>Console.WriteLine(MathDecimals.GetDecimalPlaces((decimal)-0.01f));       // 2</para>
        /// </returns>
        public static int DecimalPlaces(this decimal @this)
        {
            @this = Math.Abs(@this); //make sure it is positive.
            @this -= (int)@this;     //remove the integer part of the number.
            var decimalPlaces = 0;
            while (@this > 0)
            {
                decimalPlaces++;
                @this *= 10;
                @this -= (int)@this;
            }
            return decimalPlaces;
        }

    }
}