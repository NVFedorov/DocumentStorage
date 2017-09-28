// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataConfig.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DataConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.DAL
{
    using System.Reflection;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Tool.hbm2ddl;

    /// <summary>
    /// The NHibernate helper provides methods to initialize NHibernate.
    /// </summary>
    internal class NHibernateHelper
    {
        /// <summary>
        /// The open session.
        /// </summary>
        /// <returns>
        /// The <see cref="ISession"/>.
        /// </returns>
        public static ISession OpenSession()
        {
            var cfg = new Configuration().Configure(); 

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            new SchemaUpdate(cfg).Execute(true, true);
            var sessionFactory = cfg.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
