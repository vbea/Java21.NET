using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class Address
    {
        /// <summary>
        /// 省
        /// </summary>
        public string Prov { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public string Dist { get; set; }

        public void clear()
        {
            Prov = City = Dist = "";
        }
    }
}
