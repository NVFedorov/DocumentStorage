using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentStorage.DAL.Data
{
    public class Document
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual DateTimeOffset CreationDate { get; protected set; }
        public virtual User Author { get; set; }
        public virtual byte[] FileContent { get; set; }
    }
}
