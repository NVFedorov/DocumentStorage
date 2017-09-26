// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateFileAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ValidateFileAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using DocumentStorage.Helpers;

    /// <summary>
    /// The validate file attribute.
    /// </summary>
    public class ValidateFileAttribute : RequiredAttribute
    {
        /// <summary>
        /// Checks whether the file is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            return file.ContentLength <= ConfigHelper.MaxFileSize;
        }
    }
}