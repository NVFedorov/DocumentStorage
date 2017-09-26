// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DocumentViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Models.Document
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using DocumentStorage.CustomAttributes;
    using DocumentStorage.Helpers;

    /// <summary>
    /// The document view model.
    /// </summary>
    public class DocumentViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [Required]
        [ValidateFile(ErrorMessage = "The maximum file size allowed: 10 MB")]
        public HttpPostedFileBase File { get; set; }
    }
}