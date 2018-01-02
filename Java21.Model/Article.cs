using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class Article
    {
        public int id { get; set; }
        public string title { get; set; }
        public int category { get; set; }
        public string catename { get; set; }
        public string artical { get; set; }
        public Nullable<DateTime> cdate { get; set; }
        public string cuser { get; set; }
        public Nullable<DateTime> edate { get; set; }
        public string euser { get; set; }
        public Nullable<DateTime> comment { get; set; }
        public int cread { get; set; }
        public bool valid { get; set; }
        public List<Comment> comments { get; set; }
        public int comcount { get; set; }
    }
}
