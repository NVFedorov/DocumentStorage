// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentMap.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DocumentMap type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.DAL.Data
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// The document map.
    /// </summary>
    public class DocumentMap : ClassMapping<Document>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMap"/> class.
        /// </summary>
        public DocumentMap()
        {
            this.Lazy(false);
            this.Table("Documents");
            this.Id(x => x.Id, map => map.Generator(Generators.Guid));
            this.Property(x => x.Name);
            this.Property(x => x.CreationDate);
            this.ManyToOne(
                x => x.Author,
                c =>
                    {
                        c.Cascade(Cascade.Persist);
                        c.Column("AuthorId");
                    });

            this.Property(
                x => x.FileContent,
                map =>
                    {
                        map.Column(cm =>
                            {
                                cm.SqlType("varbinary(max)");
                                cm.Length(int.MaxValue);
                            });
                        map.Type(NHibernateUtil.BinaryBlob);
                        map.Length(int.MaxValue);
                    });
        }
    }
}
