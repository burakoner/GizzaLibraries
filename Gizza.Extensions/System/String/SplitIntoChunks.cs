using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string[] SplitIntoChunks(this string @this, int chunkSize)
        {
            //splits string into array with chunks of given size. not really that useful..
            if (string.IsNullOrEmpty(@this))
                return new string[] { "" };

            int stringLength = @this.Length;

            int chunksRequired = (int)Math.Ceiling((decimal)stringLength / (decimal)chunkSize);
            var stringArray = new string[chunksRequired];

            int lengthRemaining = stringLength;

            for (int i = 0; i < chunksRequired; i++)
            {
                int lengthToUse = Math.Min(lengthRemaining, chunkSize);
                int startIndex = chunkSize * i;
                stringArray[i] = @this.Substring(startIndex, lengthToUse);

                lengthRemaining = lengthRemaining - lengthToUse;
            }

            return stringArray;
        }
    }
}