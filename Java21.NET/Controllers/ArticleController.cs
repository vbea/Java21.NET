using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Vbes.WebControls.Mvc;

namespace Java21.NET.Controllers
{
    public class ArticleController : Controller
    {
        private bool isLogin()
        {
            return Session["LoginUser"] != null;
        }

        public ActionResult Views(string id)
        {
            if (Common.IsMobile(Request.ServerVariables["HTTP_USER_AGENT"]))
                return RedirectToActionPermanent("java", "article", new { id = id });
            if (isLogin())
            {
                ViewData.Add("logins", "");
                ViewData.Add("vilid", "");
            }
            else
            {
                ViewData.Add("logins", "你需要登录后才能发表");
                ViewData.Add("vilid", "disabled=disabled");
            }
            if (id.ToLower().Equals("views"))
                return RedirectToAction("article", "Java21");
            int aid = Convert.ToInt32(id);
            JavaDLL dll = new JavaDLL();
            Article mod = dll.getArticle(aid);
            DataSet userds = dll.getAllUser();
            int count = 0;
            List<Comment> comments = dll.getComments(aid, 10, 1, out count);
            //mod.comments = dll.getComments(aid, 10, 1, out count);
            mod.comcount = count;
            //linq级联查询
            var comment = from u in userds.Tables[0].AsEnumerable()
                          join c in comments on u["name"].ToString() equals c.uid
                          orderby c.cdate descending
                          select new Comment { id = c.id, aid = c.aid, sid=u["name"].ToString(), uid = u["nickname"].ToString(), comment = Convert.ToBoolean(u["valid"]) ? c.comment : "<span>用户被屏蔽，内容自动删除</span>", cdate = c.cdate, device = c.device, head = u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString() };
            mod.comments = comment.ToList<Comment>();
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["share"]))
                ViewData.Add("share", "0");
            return View(mod);
        }

        [HttpPost]
        public string Views(string id, string result)
        {
            if (isLogin())
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                int aid = Convert.ToInt32(id);
                string txt = Request.Form["txtComment"];
                JavaDLL dll = new JavaDLL();
                if (Session["LoginDevice"] != null)
                    dll.addComment(aid, user.UserName, txt, Session["LoginDevice"].ToString());
                else
                    dll.addComment(aid, user.UserName, txt);
                DataSet ds = dll.getComment(aid);
                result = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result += "<div class=\"comment\">\n";
                    result += "<div class=\"head\">\n";
                    result += "<div id=\"head\"><img src=\"" + (row["head"].Equals(DBNull.Value) ? "/images/head.jpg" : row["head"]) + "\" /></div>\n";
                    if (Convert.ToBoolean(row["valid"]))
                        result += "<div id=\"uid\"><a href=\"/users/detail/" + row["name"] + "\">" + row["nickname"] + "</a><span id=\"device\">" + row["device"] + "</span></div>\n";
                    else
                        result += "<div id=\"uid\">" + row["nickname"] + "<span id=\"device\">" + row["device"] + "</span></div>\n";
                    result += "<div id=\"cdate\">" + Convert.ToDateTime(row["cdate"]).ToString("MM-dd HH:mm") + "</div>\n</div>";
                    if (Convert.ToBoolean(row["valid"]))
                        result += "<div id=\"comment\">" + show_content(row["comment"].ToString()) + "</div>\n";
                    else
                        result += "<div id=\"comment\"><span>用户被屏蔽，内容自动删除</span></div>\n";
                    result += "</div>";
                }
                Thread.Sleep(1000);
                return result;
            }
            else
            {
                Response.Redirect("/article");
                return "";
            }
        }

        public ActionResult Comment(int id, int? p)
        {
            int pageindex = p ?? 1;
            int pagesize = 20;
            int total = 0;
            JavaDLL dll = new JavaDLL();
            DataSet userds = dll.getAllUser();
            ArticleEx mod = new ArticleEx();
            Article art = dll.getArticleNotRead(id);
            List<Comment> comments = dll.getComments(id, pagesize, pageindex, out total);
            //linq级联查询
            var comment = from u in userds.Tables[0].AsEnumerable()
                          join m in comments on u["name"].ToString() equals m.uid
                          orderby m.cdate descending
                          select new Comment { id = m.id, aid = m.aid, sid = u["name"].ToString(), uid = u["nickname"].ToString(), comment = Convert.ToBoolean(u["valid"]) ? m.comment : "<span>用户被屏蔽，内容自动删除</span>", cdate = m.cdate, device = m.device, head = u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString() };
            comments = comment.ToList<Comment>();
            PagedList<Comment> page = comments.AsQueryable().ToPagedList(pageindex, pagesize);
            page.TotalItemCount = total;
            page.CurrentPageIndex = pageindex;
            mod.id = id;
            mod.category = art.category;
            mod.title = art.title;
            mod.comments = page;
            return View(mod);
        }

        [HttpPost]
        public ActionResult Comment(int id)
        {
            if (isLogin())
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                int aid = Convert.ToInt32(id);
                string txt = Request.Form["txtComment"];
                JavaDLL dll = new JavaDLL();
                if (Session["LoginDevice"] != null)
                    dll.addComment(aid, user.UserName, txt, Session["LoginDevice"].ToString());
                else
                    dll.addComment(aid, user.UserName, txt);
                return Comment(id, null);
            }
            else
            {
                return RedirectToAction("article", "Java21");
            }
        }

        public ActionResult c(int id, int? p)
        {
            int pageindex = p ?? 1;
            int pagesize = 20;
            int total = 0;
            JavaDLL dll = new JavaDLL();
            DataSet userds = dll.getAllUser();
            ArticleEx mod = new ArticleEx();
            Article art = dll.getArticleNotRead(id);
            List<Comment> comments = dll.getComments(id, pagesize, pageindex, out total);
            //linq级联查询
            var comment = from u in userds.Tables[0].AsEnumerable()
                          join c in comments on u["name"].ToString() equals c.uid
                          orderby c.cdate descending
                          select new Comment { id = c.id, aid = c.aid, sid = u["name"].ToString(), uid = u["nickname"].ToString(), comment = Convert.ToBoolean(u["valid"]) ? c.comment : "<span>用户被屏蔽，内容自动删除</span>", cdate = c.cdate, device = c.device, head = u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString() };
            comments = comment.ToList<Comment>();
            PagedList<Comment> page = comments.AsQueryable().ToPagedList(pageindex, pagesize);
            page.TotalItemCount = total;
            page.CurrentPageIndex = pageindex;
            mod.id = id;
            mod.category = art.category;
            mod.title = art.title;
            mod.comments = page;
            return View(mod);
        }

        [HttpPost]
        public ActionResult c(int id)
        {
            if (isLogin())
            {
                UserInfo user = (UserInfo)Session["LoginUser"];
                int aid = Convert.ToInt32(id);
                string txt = Request.Form["txtComment"];
                JavaDLL dll = new JavaDLL();
                if (Session["LoginDevice"] != null)
                    dll.addComment(aid, user.UserName, txt, Session["LoginDevice"].ToString());
                else
                    dll.addComment(aid, user.UserName, txt);
                return c(id, null);
            }
            else
            {
                return java(id.ToString(), null, null);
            }
        }

        public ActionResult java(string id, string client, string device)
        {
            if (id.ToLower().Equals("views"))
                return RedirectToAction("article", "Java21");
            int aid = Convert.ToInt32(id);
            JavaDLL dll = new JavaDLL();
            Article mod = dll.getArticle(aid);
            if (device != null)
                Session["LoginDevice"] = device;
            DataSet userds = dll.getAllUser();//获取所有用户信息
            string[] clients = null;
            if (client != null)
                clients = client.Split('_');
            UserInfo info = (UserInfo)Session["LoginUser"];
            if (info == null || (client != null && !info.ID.Equals(clients[1])))
            {
                if (clients != null && clients.Length == 2)
                {
                    var client_user = from u in userds.Tables[0].AsEnumerable()
                                      where u["psd"].ToString().Equals(clients[0]) && u["id"].ToString().Equals(clients[1])
                                      select new UserInfo
                                      {
                                          ID = Convert.ToInt32(u["id"]),
                                          UserName = u["name"].ToString(),
                                          UserPass = u["psd"].ToString(),
                                          Role = Convert.ToInt32(u["roles"]),
                                          Gender = Convert.ToInt32(u["gender"]),
                                          NickName = u["nickname"].ToString(),
                                          Email = u["email"].ToString(),
                                          QQ = "" + u["qq"],
                                          Mobile = "" + u["mobile"],
                                          Birthday = Convert.ToDateTime(u["birthday"]),
                                          Addr = "" + u["address"],
                                          Valid = Convert.ToBoolean(u["valid"]),
                                          Mark = "" + u["mark"],
                                          HeadImg = (u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString()),
                                      };
                    info = client_user.FirstOrDefault<UserInfo>();
                    if (info != null)
                    {
                        info.SetAddress(info.Addr);
                        Session["LoginUser"] = info;
                    }
                }
            }
            //判断是否登录
            if (info != null)
            {
                ViewData.Add("logins", "");
                ViewData.Add("vilid", "");
            }
            else
            {
                ViewData.Add("logins", "你需要登录后才能发表");
                ViewData.Add("vilid", "disabled=disabled");
            }
            int count = 0;
            List<Comment> comments = dll.getComments(aid, 10, 1, out count);
            //mod.comments = dll.getComments(aid, 10, 1, out count);
            mod.comcount = count;
            //linq级联查询
            var comment = from u in userds.Tables[0].AsEnumerable()
                          join c in comments on u["name"].ToString() equals c.uid
                          orderby c.cdate descending
                          select new Comment { id = c.id, aid = c.aid, sid = u["name"].ToString(), uid = u["nickname"].ToString(), comment = Convert.ToBoolean(u["valid"]) ? c.comment : "<span>用户被屏蔽，内容自动删除</span>", cdate = c.cdate, device = c.device, head = u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString() };
            mod.comments = comment.ToList<Comment>();
            return View(mod);
        }

        public ActionResult Video(int? id, string client, string device)
        {
            int d = id ?? 1;
            JavaDLL dll = new JavaDLL();
            Model.Video mod = dll.getVideo(d);
            if (mod == null)
                return View();
            if (device != null)
                Session["LoginDevice"] = device;
            DataSet userds = dll.getAllUser();//获取所有用户信息
            string[] clients = null;
            if (client != null)
                clients = client.Split('_');
            UserInfo info = (UserInfo)Session["LoginUser"];
            if (info == null || (client != null && !info.ID.Equals(clients[1])))
            {
                if (clients != null && clients.Length == 2)
                {
                    var client_user = from u in userds.Tables[0].AsEnumerable()
                                      where u["psd"].ToString().Equals(clients[0]) && u["id"].ToString().Equals(clients[1])
                                      select new UserInfo
                                      {
                                          ID = Convert.ToInt32(u["id"]),
                                          UserName = u["name"].ToString(),
                                          UserPass = u["psd"].ToString(),
                                          Role = Convert.ToInt32(u["roles"]),
                                          Gender = Convert.ToInt32(u["gender"]),
                                          NickName = u["nickname"].ToString(),
                                          Email = u["email"].ToString(),
                                          QQ = "" + u["qq"],
                                          Mobile = "" + u["mobile"],
                                          Birthday = Convert.ToDateTime(u["birthday"]),
                                          Addr = "" + u["address"],
                                          Valid = Convert.ToBoolean(u["valid"]),
                                          Mark = "" + u["mark"],
                                          HeadImg = (u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString()),
                                      };
                    info = client_user.FirstOrDefault<UserInfo>();
                    if (info != null)
                    {
                        info.SetAddress(info.Addr);
                        Session["LoginUser"] = info;
                    }
                }
            }
            //判断是否登录
            if (info != null)
            {
                ViewData.Add("logins", "");
                ViewData.Add("vilid", "");
            }
            else
            {
                ViewData.Add("logins", "你需要登录后才能发表");
                ViewData.Add("vilid", "disabled=disabled");
            }
            int count = 0;
            List<Comment> comments = dll.getComments(d, 20, 1, out count);
            //mod.comments = dll.getComments(aid, 10, 1, out count);
            mod.comcount = count;
            //linq级联查询
            var comment = from u in userds.Tables[0].AsEnumerable()
                          join c in comments on u["name"].ToString() equals c.uid
                          orderby c.cdate descending
                          select new Comment { id = c.id, aid = c.aid, sid = u["name"].ToString(), uid = u["nickname"].ToString(), comment = Convert.ToBoolean(u["valid"]) ? c.comment : "<span>用户被屏蔽，内容自动删除</span>", cdate = c.cdate, device = c.device, head = u["head"].Equals(DBNull.Value) ? "/images/head.jpg" : u["head"].ToString() };
            mod.comments = comment.ToList<Comment>();
            return View(mod);
        }

        public string show_content(string str)
        {
            str = Regex.Replace(str, @"\<", "&lt;");
            str = Regex.Replace(str, @"\>", "&gt;");
            str = Regex.Replace(str, @"\n", "<br/>");
            str = str.Replace(" ", "&nbsp;");
            str = Regex.Replace(str, @"\[em_([0-9]*)\]", "<img src='/images/arclist/$1.gif' border='0' />");
            return str;
        }
    }
}
