using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Feature.Postcard.Models
{
    public class BlogArticle
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string LongDescription { get; set; }
        public virtual string ShortImage { get; set; }
        public virtual string LargeImage { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string Category { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual string Author { get; set; }
    }
}