// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SearchViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Models.Document
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The search view model.
    /// </summary>
    public class SearchViewModel
    {
        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        public string SearchString { get; set; }
    }
}