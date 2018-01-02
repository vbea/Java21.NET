using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class Video
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string url2 { get; set; }
        public List<Comment> comments { get; set; }
        public int comcount { get; set; }
    }
}
