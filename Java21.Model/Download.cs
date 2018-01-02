using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class Download
    {
        public int id { get; set; }
        public string version { get; set; }
        public string url { get; set; }
        public DateTime cdate { get; set; }
    }
}
