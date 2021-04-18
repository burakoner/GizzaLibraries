using System.IO;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// Recursively create directory
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
    }
}