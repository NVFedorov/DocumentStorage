// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.DAL.Repository
{
    using System;
    using System.Linq;

    using DocumentStorage.DAL.Data;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The user repository provides methods to work with the System_Users table in the database.
    /// </summary>
    public static class UserRepository
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// True if user was successfully created.
        /// </returns>
        public static bool Create(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec sp_add_user @UserName=:username, @Password=:password");
                query.SetString("username", user.UserName);
                query.SetString("password", user.Password);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }
        }

        /// <summary>
        /// Gets user by name.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public static User GetUserByName(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var user = session.Query<User>().FirstOrDefault(x => x.UserName == username);
                return user;
            }
        }
    }
}
