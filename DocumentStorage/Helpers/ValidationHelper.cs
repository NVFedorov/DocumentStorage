// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ValidationHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Helpers
{
    using System;

    using DocumentStorage.DAL.Data;
    using DocumentStorage.DAL.Repository;

    /// <summary>
    /// The validation helper.
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Encodes the source string to the SHA1.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Encode(string source)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(source ?? string.Empty);
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", string.Empty);
        }

        /// <summary>
        /// Validates the specified user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ValidateUser(User user)
        {
            var dbUser = UserRepository.GetUserByName(user.UserName);
            if (dbUser == null)
            {
                return false;
            }

            return user.Password == dbUser.Password;
        }
    }
}