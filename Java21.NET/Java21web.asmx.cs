using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Java21.Logic;
using System.Data;
using Newtonsoft.Json.Linq;
using Java21.Model;
using System.Web.Security;
using Java21.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;

namespace Java21.NET
{
    /// <summary>
    /// Java21web 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://vbes.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class Java21web : System.Web.Services.WebService
    {
        [WebMethod(Description = "创建一个注册码")]
        public string CreateKey(string key, string version, string max, string mark, string password)
        {
            JavaDLL dll = new JavaDLL();
            if (dll.Login("admin").UserPass.Equals(password))
            {
                return dll.addKeys(key, Convert.ToInt32(max), version, mark).ToString();
            }
            else
                return "Password error";
        }

        [WebMethod(Description = "获取指定注册码的信息")]
        public string GetKeyInfo(string key)
        {
            //Context.Response.ContentType = "text/plain";
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getKey(key);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                var keyvalue = new
                {
                    keys = new
                    {
                        key = row["keys"],
                        password = "java21",
                        time = (Convert.ToInt32(row["maxc"]) - Convert.ToInt32(row["curr"])),
                        version = row["ver"],
                        date = Convert.ToDateTime(row["cdate"]).ToString("yyyy-MM-dd"),
                        option = row["mark"],
                        response = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                };
                JObject keyjson = JObject.FromObject(keyvalue);
                return keyjson.ToString();
            }
            else
                return "No keys";
        }

        [WebMethod(Description = "提交一个注册码进行注册")]
        public bool RegistKey(string key)
        {
            return new JavaDLL().registKey(key);
        }

        [WebMethod(Description = "获取一个免费的注册码")]
        public string GetKey()
        {
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getKey();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "No keys";
        }

        [WebMethod(Description = "获取公告信息")]
        public string GetQuotations()
        {
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getQuotations();
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                Random rand = new Random();
                return ds.Tables[0].Rows[rand.Next(0, count)]["sentence"].ToString();
            }
            else
                return "-";
        }

        [WebMethod(Description="注册用户")]
        public string RegisterUser(string name, string psd, string nickname, int gender, string email)
        {
            if (!IsUserName(name))
                return "用户名输入不正确";
            if (!IsEmail(email))
                return "邮箱输入不正确";
            UserInfo info = new UserInfo();
            info.UserName = name;
            info.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(psd), "MD5");
            info.Role = UserInfo.ROLE_USER;
            info.Valid = true;
            info.NickName = nickname;
            info.Email = email;
            info.Gender = gender;
            info.HeadImg = "/images/head.jpg";
            info.Mark = "他很懒，什么也没留下";
            JavaDLL dll = new JavaDLL();
            if (dll.checkUserforName(info.UserName))
            {
                if (dll.checkUserforEmail(info.Email))
                {
                    if (dll.Register(info))
                    {
                        Log.e("Sign In:" + info.UserName + "(" + psd + ")(" + email + ")", ConfigurationManager.AppSettings["logPath"] + "\\user_appregist_" + DateTime.Now.ToString("yyyyMM") + ".log", DateTime.Now.ToString("MM-dd HH:mm:ss"));
                        return "true";
                    }
                    else
                        return "注册失败，请重试";
                }
                else
                    return "该邮箱已经存在";
            }
            else
                return "该用户名已经存在";
        }

        [WebMethod(Description = "登录")]
        public string Login(string name, string sid, string device, bool auto)
        {
            string pasd = auto ? sid : FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(sid), "MD5");
            JavaDLL bll = new JavaDLL();
            UserInfo info = bll.Login(name);
            if (info != null && info.Valid)
            {
                if (info.UserPass.Equals(pasd) && info.Role != UserInfo.ROLE_CONF)
                {
                    var json = new
                    {
                        uid = info.ID,
                        user = info.UserName,
                        name = info.NickName,
                        sid = info.UserPass,
                        role = info.Role,
                        gender = info.Gender,
                        email = info.Email,
                        qq = info.QQ,
                        mobile = info.Mobile,
                        birth = info.Birthday.ToString("yyyy-MM-dd"),
                        address = info.Addr,
                        mark = info.Mark,
                        head = ""+info.HeadImg
                    };
                    JObject keyjson = JObject.FromObject(json);
                    Log.e("Login:" + info.UserName + "\r\n\tDevice:" + device, ConfigurationManager.AppSettings["logPath"] + "\\user_applogin_" + DateTime.Now.ToString("yyyyMM") + ".log", DateTime.Now.ToString("MM-dd HH:mm:ss"));
                    //HttpContext.Current.Session["LoginUser"] = info;
                    //HttpContext.Current.Session["LoginDevice"] = device;
                    return keyjson.ToString();
                }
                else
                    return "true";
            }
            else
                return "false";
        }

        [WebMethod(Description = "注销", EnableSession = true)]
        public void Logout()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        [WebMethod(Description = "获取知识点列表")]
        public string GetKnowledge(int page)
        {
            if (page > 0)
            {
                JavaDLL bll = new JavaDLL();
                DataSet ds = bll.getArticle(20, page);
                var json = new
                {
                    page = page,
                    count = ds.Tables[0].Rows.Count,
                    list = new List<object>()
                };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var _row = new
                    {
                        id = row["id"],
                        title = row["title"],
                        date = Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm"),
                        read = row["cread"]
                    };
                    json.list.Add(_row);
                }
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        [WebMethod(Description = "获取文章列表")]
        public string GetArticle(int page)
        {
            if (page > 0)
            {
                JavaDLL bll = new JavaDLL();
                //DataSet ds = bll.getArticle(20, page);
                //2016.06.03
                var json = new
                {
                    page = page,
                    count = 0,
                    list = new List<object>()
                };
                /*foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var _row = new
                    {
                        id = row["id"],
                        cate = row["category"],
                        title = row["title"],
                        date = Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm"),
                        read = row["cread"]
                    };
                    json.list.Add(_row);
                }*/
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        public string GetArticleNew(int page)
        {
            if (page > 0)
            {
                JavaDLL bll = new JavaDLL();
                DataSet ds = bll.getArticle(20, page);
                //2016.06.03
                var json = new
                {
                    page = page,
                    count = 0,
                    list = new List<object>()
                };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var _row = new
                    {
                        id = row["id"],
                        cate = row["category"],
                        title = row["title"],
                        date = Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm"),
                        read = row["cread"]
                    };
                    json.list.Add(_row);
                }
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        [WebMethod(Description = "根据分类获取文章列表")]
        public string GetCateArticle(int page, int cate)
        {
            if (page > 0)
            {
                JavaDLL bll = new JavaDLL();
                //DataSet ds = bll.getArticle(cate, 20, page);
                var json = new
                {
                    page = page,
                    count = 0,//ds.Tables[0].Rows.Count,
                    list = new List<object>()
                };
                /*foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var _row = new
                    {
                        id = row["id"],
                        cate = row["category"],
                        title = row["title"],
                        date = Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm"),
                        read = row["cread"]
                    };
                    json.list.Add(_row);
                }*/
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        public string GetCateArticleNew(int page, int cate)
        {
            if (page > 0)
            {
                JavaDLL bll = new JavaDLL();
                DataSet ds = bll.getArticle(cate, 20, page);
                var json = new
                {
                    page = page,
                    count = ds.Tables[0].Rows.Count,
                    list = new List<object>()
                };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var _row = new
                    {
                        id = row["id"],
                        cate = row["category"],
                        title = row["title"],
                        date = Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm"),
                        read = row["cread"]
                    };
                    json.list.Add(_row);
                }
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        [WebMethod(Description="获取文章分类")]
        public string GetCategory()
        {
            JavaDLL bll = new JavaDLL();
            DataSet ds = bll.getCategory();
            var json = new
            {
                count = ds.Tables[0].Rows.Count,
                list = new List<object>()
            };
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var _row = new
                {
                    id = row["id"],
                    name = row["catename"]
                };
                json.list.Add(_row);
            }
            JObject keyjson = JObject.FromObject(json);
            return keyjson.ToString();
        }

        [WebMethod(Description="获取视频列表")]
        public string GetVideoList(int page)
        {
            if (page > 0)
            {
                int t = 1;
                JavaDLL bll = new JavaDLL();
                //List<Model.Video> list = bll.getAllVideo(20, page, out t);
                var json = new
                {
                    page = page,
                    total = t,
                    count = 1,//list.Count,
                    list = new List<object>()
                };
                /*foreach (Model.Video row in list)
                {*/
                    var _row = new
                    {
                        id = "51",
                        title = "视频服务已经终止",
                    };
                    json.list.Add(_row);
                //}
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        public string GetVideoListNew(int page)
        {
            if (page > 0)
            {
                int t = 0;
                JavaDLL bll = new JavaDLL();
                List<Model.Video> list = bll.getAllVideo(20, page, out t);
                var json = new
                {
                    page = page,
                    total = t,
                    count = list.Count,
                    list = new List<object>()
                };
                foreach (Model.Video row in list)
                {
                    var _row = new
                    {
                        id = row.id,
                        title = row.name,
                    };
                    json.list.Add(_row);
                }
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        [WebMethod(Description="获取资料下载列表")]
        public string GetMaterial(int page, int uid)
        {
            if (page > 0)
            {
                int t = 0;
                JavaDLL bll = new JavaDLL();
                UserInfo info = bll.Login(uid);
                List<Material> list = bll.getMaterialList(20, page, out t);
                var json = new
                {
                    page = page,
                    total = t,
                    count = list.Count,
                    list = new List<object>()
                };
                foreach (Material row in list)
                {
                    var _row = new
                    {
                        id = row.id,
                        title = row.title,
                        url = "http://vbea.wicp.net/material/"+row.id+".html",
                        psd = info.Role != UserInfo.ROLE_USER ? row.psd : "",
                        date = Convert.ToDateTime(row.cdate).ToString("yyyy-MM-dd HH:mm"),
                        remark = row.remark
                    };
                    json.list.Add(_row);
                }
                JObject keyjson = JObject.FromObject(json);
                return keyjson.ToString();
            }
            else
                return "null";
        }

        [WebMethod(Description = "提交反馈")]
        public bool Feedback(string suggest, string contact, string device)
        {
            JavaDLL bll = new JavaDLL();
            return bll.addFeedback(suggest, contact, device);
        }

        [WebMethod(Description="修改用户资料")]
        public bool SetUserinfo(int uid, string sid, string key, string value)
        {
            JavaDLL bll = new JavaDLL();
            UserInfo info = bll.Login(uid);
            if (info.UserPass.Equals(sid))
            {
                switch (key)
                {
                    case "NICK"://昵称
                        info.NickName = value;
                        break;
                    case "SEX"://性别
                        info.Gender = Convert.ToInt32(value);
                        break;
                    case "BIRTH"://生日
                        info.Birthday = Convert.ToDateTime(value);
                        break;
                    case "QQ"://QQ
                        info.QQ = value;
                        break;
                    case "PHONE"://手机
                        info.Mobile = value;
                        break;
                    case "ADDRESS"://地址
                        info.SetAddress(value.Trim('-'));
                        break;
                    case "MARK"://签名
                        info.Mark = value;
                        break;
                }
                return bll.updateFillUser(info);
            }
            return false;
        }

        [WebMethod(Description="修改用户密码")]
        public string SetUserpass(int uid, string sid, string psd)
        {
            if (sid.Equals(psd))
                return "密码未修改";
            string old = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(sid), "MD5");
            JavaDLL bll = new JavaDLL();
            UserInfo info = bll.Login(uid);
            if (info.UserPass.Equals(old))
            {
                info.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(psd), "MD5");
                if (bll.changePassword(info.ID, info.UserPass))
                    return "修改成功";
                else
                    return "修改失败";
            }
            else
                return "原密码输入不正确";
        }

        [WebMethod(Description="设置用户头像")]
        public bool SetHead(byte[] fileBytes, string name, int uid, string sid)
        {
            try
            {
                string path = HttpContext.Current.Request.PhysicalApplicationPath + "portrait";
                JavaDLL dll = new JavaDLL();
                UserInfo info = dll.Login(uid);
                if (info != null && info.UserPass.Equals(sid) || true)
                {
                    path += "\\" + name;
                    MemoryStream memoryStream = new MemoryStream(fileBytes); //1.定义并实例化一个内存流，以存放提交上来的字节数组。  
                    FileStream fileUpload = new FileStream(path, FileMode.Create); ///2.定义实际文件对象，保存上载的文件。  
                    memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件  
                    memoryStream.Close();
                    fileUpload.Close();
                    fileUpload = null;
                    memoryStream = null;
                    if (new FileInfo(path).Exists)
                    {
                        info.HeadImg = "/portrait/" + name;
                        return dll.updateUser(info);
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsUserName(string input)
        {
            string regex = @"^[A-Za-z0-9]+$";
            return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
        }

        public static bool IsEmail(string input)
        {
            //string regex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            string regex = @"^(?:[a-z\d]+[_\-\+\.]?)*[a-z\d]+@(?:([a-z\d]+\-?)*[a-z\d]+\.)+([a-z]{2,})+$";
            string[] strs = input.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                if (!Regex.IsMatch(strs[i], regex, RegexOptions.IgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
