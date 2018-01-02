using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class JavaKeyEx
    {
        /// <summary>
        /// 注册码
        /// </summary>
        public string keys { get; set; }
        /// <summary>
        /// 剩余次数
        /// </summary>
        public int residue { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ver { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public int rdate { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool valid { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string mark { get; set; }
    }
}
