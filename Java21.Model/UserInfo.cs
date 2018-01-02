using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Java21.Model
{
    public class UserInfo
    {
        public UserInfo()
        {
            Birthday = new DateTime(1990, 1, 1);
        }
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPass { get; set; }
        /// <summary>
        /// 性别（0:男;1:女）
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int Role { get; set; }
        public string RoleStr { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 居住地
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public const int ROLE_ADMIN = 1;
        /// <summary>
        /// 普通用户
        /// </summary>
        public const int ROLE_USER = 2;
        /// <summary>
        /// VIP用户
        /// </summary>
        public const int ROLE_VIP = 3;
        /// <summary>
        /// 受限用户
        /// </summary>
        public const int ROLE_CONF = 4;
        

        public void SetAddress(string s)
        {
            Addr = s;
            if (Address == null)
                Address = new Address();
            Address.clear();
            if (!s.Equals(""))
            {
                string[] addr = addr = s.Split('-');
                switch (addr.Length)
                { 
                    case 1:
                        Address.Prov = addr[0];
                        break;
                    case 2:
                        Address.Prov = addr[0];
                        Address.City = addr[1];
                        break;
                    case 3:
                        Address.Prov = addr[0];
                        Address.City = addr[1];
                        Address.Dist = addr[2];
                    break;
                }
            }
        }

        public void SetAddress(string p, string c, string d)
        {
            if (Address == null)
                Address = new Address();
            else
                Address.clear();
            if (!string.IsNullOrEmpty(p))
            {
                Address.Prov = p;
                if (!string.IsNullOrEmpty(c))
                {
                    Address.City = c;
                    if (!string.IsNullOrEmpty(d))
                        Address.Dist = d;
                }
                Addr = GetAddress();
            }
        }

        public string GetAddress()
        {
            if (Address.Prov == null)
                return "";
            else
            {
                string result = Address.Prov;
                if (Address.City != null)
                {
                    result += "-" + Address.City;
                    if (Address.Dist != null)
                        result += "-" + Address.Dist;
                }
                return result.Trim('-');
            }
        }
    }
}
