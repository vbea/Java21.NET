using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class Comment
    {
        public int id { get; set; }
        public int aid { get; set; }
        public string uid { get; set; }
        public string sid { get; set; }
        public int star { get; set; }
        public string comment { get; set; }
        public DateTime cdate { get; set; }
        public string device { get; set; }
        public string head { get; set; }
    }
}
