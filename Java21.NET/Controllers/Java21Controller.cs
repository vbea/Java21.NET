using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Java21.Model;
using Java21.Logic;
using Vbes.WebControls.Mvc;
using System.Data;

namespace Java21.NET.Controllers
{
    public class Java21Controller : Controller
    {
        //
        // GET: /Java21/
        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <returns></returns>
        private bool isLogin()
        {
            return Session["LoginUser"] != null;
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Users()
        {
            UserInfo user = (UserInfo)Session["LoginUser"];
            if (user != null)
            {
                user = new JavaDLL().Login(user.ID);
                return View(user);
            }
            else
                return Redirect("/users/login");
        }

        public ActionResult Download(int? p)
        {
            if (Common.IsMobile(Request.ServerVariables["HTTP_USER_AGENT"]))
                ViewData.Add("url", "http://a.app.qq.com/o/simple.jsp?pkgname=com.vbea.java21");
            else
                ViewData.Add("url", "http://android.myapp.com/myapp/detail.htm?apkName=com.vbea.java21");
            int pageindex = p ?? 1;
            int pagesize = 15;
            int total = 0;
            JavaDLL dll = new JavaDLL();
            PagedList<Download> page = dll.getDownloadList(pagesize, pageindex, out total).AsQueryable().ToPagedList(pageindex, pagesize);
            page.TotalItemCount = total;
            page.CurrentPageIndex = pageindex;
            return View(page);
        }

        public ActionResult JavaKeys(int? p)
        {
            int pageindex = p ?? 1;
            int pagesize = 20;
            int total = 0;
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getAllKeys(pagesize,pageindex, out total);
            List<JavaKeyEx> list = new List<JavaKeyEx>();
            UserInfo info = (UserInfo)Session["LoginUser"];
            int role = 0;
            if (info != null)
            {
                role = info.Role;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                JavaKeyEx key = new JavaKeyEx();
                key.ver = row["ver"].Equals("p") ? "专业版" : "标准版";
                key.keys = showKeys(role, row["ver"].Equals("p"), row["keys"].ToString());
                key.residue = Convert.ToInt32(row["maxc"]) - Convert.ToInt32(row["curr"]);
                key.rdate = (int)(Convert.ToDateTime(row["cdate"]).AddMonths(1) - DateTime.Now).TotalDays;
                key.mark = "" + row["mark"];
                if (key.rdate < 0)
                    key.rdate = 0;
                if (key.rdate == 0 || key.residue == 0)
                    key.valid = false;
                else
                    key.valid = true;
                list.Add(key);
            }
            PagedList<JavaKeyEx> page = list.AsQueryable().ToPagedList(pageindex, pagesize);
            page.TotalItemCount = total;
            page.CurrentPageIndex = pageindex;
            return View(page);
        }

        //public ActionResult Article(int? p)
        //{
        //    int pageindex = p ?? 1;
        //    int pagesize = 10;
        //    int total = 0;
        //    ArticleList list = new ArticleList();
        //    JavaDLL dll = new JavaDLL();
        //    list.articles = dll.getArticle(pagesize, pageindex, out total).AsQueryable().ToPagedList(pageindex, pagesize);
        //    list.articles.TotalItemCount = total;
        //    list.articles.CurrentPageIndex = pageindex;
        //    list.categorys = dll.getCategorys();
        //    list.category = 0;//0代表全部
        //    return View(list);
        //}

        public ActionResult Article(int? c, int? p)
        {
            int pageindex = p ?? 1;
            int cate = c ?? 0;
            int pagesize = 10;
            int total = 0;
            ArticleList list = new ArticleList();
            JavaDLL dll = new JavaDLL();
            if (cate != 0)
                list.articles = dll.getArticle(cate, pagesize, pageindex, out total).AsQueryable().ToPagedList(pageindex, pagesize);
            else
                list.articles = dll.getArticle(pagesize, pageindex, out total).AsQueryable().ToPagedList(pageindex, pagesize);
            list.articles.TotalItemCount = total;
            list.articles.CurrentPageIndex = pageindex;
            list.categorys = dll.getCategorys();
            list.category = cate;
            list.dataBind();
            return View(list);
        }

        public ActionResult Wenda()
        {
            return View();
        }

        public ActionResult About(string url)
        {
            return View();
        }

        public ActionResult Professional()
        {
            return View();
        }

        public ActionResult Feedback(string t)
        {
            if (t != null && t.Equals("true"))
                ViewData.Add("action", "提交成功");
            return View();
        }

        [HttpPost]
        public ActionResult Feedback()
        {
            if (!string.IsNullOrEmpty(Request["feedback"]))
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                JavaDLL dll = new JavaDLL();
                if (user != null)
                    dll.addFeedback("" + Request["feedback"], "" + Request["contact"], "PC_" + user.UserName);
                else
                    dll.addFeedback("" + Request["feedback"], "" + Request["contact"], "PC_游客");
            }
            return Feedback("true");
        }

        public ActionResult Material(int? p)
        {
            int pageindex = p ?? 1;
            int pagesize = 10;
            int total = 0;
            JavaDLL dll = new JavaDLL();
            PagedList<Model.Material> page = dll.getMaterialList(pagesize, pageindex, out total).AsQueryable().ToPagedList(pageindex, pagesize);
            page.TotalItemCount = total;
            page.CurrentPageIndex = pageindex;
            return View(page);
        }

        protected string showKeys(int role, bool pro, string key)
        {
            if (role == UserInfo.ROLE_ADMIN || (role == UserInfo.ROLE_USER && !pro))
                return key;
            else
            {
                string show = "";
                if (key.Length >= 20)
                {
                    show += key.Substring(0, 5);
                    show += showStar(key.Length - 10);
                    show += key.Substring(key.Length - 5);
                }
                /*else if (key.Length <= 20 && key.Length > 7)
                {
                    show += key.Substring(0, 2);
                    show += showStar(key.Length - 4);
                    show += key.Substring(key.Length - 2);
                }
                else if (key.Length <= 7 && key.Length > 4)
                {
                    show += key.Substring(0, 1);
                    show += showStar(key.Length - 2);
                    show += key.Substring(key.Length - 1);
                }*/
                else// if (key.Length <= 4)
                {
                    show += key.Substring(0, 1);
                    show += showStar(key.Length - 1);
                }
                return show;
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        private string showStar(int i)
        {
            int j = 0;
            string s = "";
            while (j < i)
            {
                s += "*";
                j++;
            }
            return s;
        }
    }
}
