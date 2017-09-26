// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentRepository.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DocumentRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.DAL.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;

    using DocumentStorage.DAL.Data;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The document repository provides access to Documents table in the database.
    /// </summary>
    public static class DocumentRepository
    {
        /// <summary>
        /// Creates new document in the database.
        /// </summary>
        /// <param name="documentName">
        /// The document name.
        /// </param>
        /// <param name="authorName">
        /// The author name.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Create(string documentName, string authorName, byte[] content)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec sp_create_document @Name=:name, @Author=:author, @FileContent=:content");
                query.SetString("name", documentName);
                query.SetString("author", authorName);
                query.SetParameter("content", content, NHibernate.Type.TypeFactory.GetBinaryType(content.Length));
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }
        }

        /// <summary>
        /// Gets all the documents from database.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Document> GetDocuments()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var docs =
                    session.Query<Document>()
                        .OrderBy(x => x.Author)
                        .ThenBy(x => x.Name)
                        .ThenBy(x => x.CreationDate)
                        .ToList();
                return docs;
            }
        }

        /// <summary>
        /// Gets the document by author and name.
        /// </summary>
        /// <param name="docName">
        /// The document name.
        /// </param>
        /// <param name="author">
        /// The authors username.
        /// </param>
        /// <returns>
        /// The <see cref="Document"/>.
        /// </returns>
        public static Document GetDocumentByAuthorAndName(string docName, string author)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var doc = session.Query<Document>().FirstOrDefault(x => x.Name == docName && x.Author.UserName == author);
                return doc;
            }
        }

        /// <summary>
        /// Searches for the documents containing the query string.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Document> SearchDocuments(string query)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var docs =
                    session.Query<Document>()
                        .Where(
                            x =>
                            x.Name.Contains(query) || x.Author.UserName.Contains(query)
                            || x.CreationDate.ToString().Contains(query))
                        .OrderBy(x => x.Author)
                        .ThenBy(x => x.Name)
                        .ThenBy(x => x.CreationDate)
                        .ToList();
                return docs;
            }
        } 
    }
}
