// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMap.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserMap type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.DAL.Data
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// The UserMap provides mapping between Users table in the DB and User business object.
    /// </summary>
    public class UserMap : ClassMapping<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.Lazy(false);
            this.Table("System_Users");
            this.Id(x => x.Id, map => map.Generator(Generators.Identity));
            this.Property(x => x.UserName);
            this.Property(x => x.Password);
        }
    }
}
