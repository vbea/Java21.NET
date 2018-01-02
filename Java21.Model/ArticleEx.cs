using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbes.WebControls.Mvc;

namespace Java21.Model
{
    public class ArticleEx
    {
        public int id { get; set; }
        public string title { get; set; }
        public int category { get; set; }
        public PagedList<Comment> comments { get; set; }
    }
}
