// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The config helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Helpers
{
    using System.Configuration;
    using System.Web.Configuration;

    /// <summary>
    /// The config helper.
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// The document display name size.
        /// </summary>
        public const int DocumentDisplayNameSize = 30;

        /// <summary>
        /// The document store path.
        /// </summary>
        public static readonly string DocumentStorePath = WebConfigurationManager.AppSettings["DocumentStoragePath"];
        
        /// <summary>
        /// The maximum file size in bytes.
        /// </summary>
        public static int MaxFileSize
        {
            get
            {
                var maxRequestLength = 10 * 1024 * 1024;
                var section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                if (section != null)
                {
                    maxRequestLength = section.MaxRequestLength * 1024;
                }

                return maxRequestLength;
            }
        }

        // TODO: add new constants here.
    }
}