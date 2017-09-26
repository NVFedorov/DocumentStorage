// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the StoreHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The store helper provides methods to save the file on the disk.
    /// </summary>
    public static class StoreHelper
    {
        /// <summary>
        /// Gets the file name with extension.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="author">
        /// The author.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFileNameWithExtension(string file, string author)
        {
            var files = Directory.GetFiles(StoreHelper.GetUserDirectory(author));
            var found = files.FirstOrDefault(x => x.Contains(file));

            return Path.GetFileName(found);
        }

        /// <summary>
        /// The get user directory.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetUserDirectory(string username)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            if (path != null)
            {
                var dirPath = Path.Combine(
                    Directory.GetParent(path).FullName,
                    ConfigHelper.DocumentStorePath,
                    username);

                return dirPath;
            }

            return string.Empty;
        }
    }
}