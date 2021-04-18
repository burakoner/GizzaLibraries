using System.IO;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        // Sample
        // DirectoryInfo di = new DirectoryInfo(@"c:\temp");
        // di.DeleteFiles("*.xml");  // Delete all *.xml files 
        // di.DeleteFiles("*.xml", true);  // Delete all, recursively

        /// <summary>
        /// Delete files in a folder that are like the searchPattern, don't include subfolders.
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <returns>Number of files that have been deleted.</returns>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern)
        {
            return DeleteFiles(di, searchPattern, false);
        }

        /// <summary>
        /// Delete files in a folder that are like the searchPattern
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <param name="includeSubdirs"></param>
        /// <returns>Number of files that have been deleted.</returns>
        /// <remarks>
        /// This function relies on DirectoryInfo.GetFiles() which will first get all the FileInfo objects in memory. This is good for folders with not too many files, otherwise
        /// an implementation using Windows APIs can be more appropriate. I didn't need this functionality here but if you need it just let me know.
        /// </remarks>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern, bool includeSubdirs)
        {
            int deleted = 0;
            foreach (FileInfo fi in di.GetFiles(searchPattern, includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }
    }
}