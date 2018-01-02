using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Java21.Data;
using Java21.Model;
using System.Data;
using System.Text.RegularExpressions;

namespace Java21.Logic
{
    public class JavaDLL
    {
        private JavaDAL dal;
        public JavaDLL()
        {
            dal = new JavaDAL(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }

        #region 用户表数据操作
        public DataSet getAllUser()
        {
            return dal.getAllUser();
        }

        public DataSet searchAllUser(string user)
        {
            return dal.searchAllUser(user);
        }

        public UserInfo Login(string user)
        {
            return dal.Login(user);
        }

        public UserInfo Login(int userid)
        {
            return dal.Login(userid);
        }

        public bool Register(UserInfo info)
        {
            return dal.Register(info) > 0;
        }

        public bool checkUserforName(string name)
        {
            return (Convert.ToInt32(dal.checkUserforName(name)) == 0);
        }

        public bool checkUserforEmail(string email)
        {
            return (Convert.ToInt32(dal.checkUserforEmail(email)) == 0);
        }

        public bool updateUser(UserInfo info)
        {
            return dal.updateUser(info) > 0;
        }

        public bool updateFillUser(UserInfo info)
        {
            return dal.updateFillUser(info) > 0;
        }

        public bool changePassword(int id, string password)
        {
            return dal.changePassword(id, password) > 0;
        }

        public bool deleteUser(int id)
        {
            return dal.deleteUser(id) > 0;
        }
        #endregion

        #region 注册码数据操作
        public DataSet getAllKeys()
        {
            return dal.getAllKeys();
        }

        public DataSet getAllKeys(int pagesize, int page, out int total)
        {
            return dal.GetList("cdate desc", pagesize, ((page - 1) * pagesize), "Keys", "1=1", out total);
        }

        public DataSet getKey(string key)
        {
            return dal.getKey(key);
        }

        public DataSet getKey()
        {
            return dal.getKey();
        }

        public bool addKeys(string key, int max, string ver, string mark)
        {
            if (Convert.ToInt32(dal.getKeyCount(key)) == 0)
                return dal.addKeys(key, max, ver, mark) > 0;
            else
                return false;
        }

        public int getKeyCount(string key)
        {
            return Convert.ToInt32(dal.getKeyCount(key));
        }

        public bool registKey(string key)
        {
            return dal.registKey(key) > 0;
        }

        public bool deleteKey(int id)
        {
            return dal.deleteKey(id) > 0;
        }
        #endregion

        #region 视频表数据操作
        public DataSet getAllVideo()
        {
            return dal.getAllVideo();
        }

        public List<Video> getAllVideos()
        {
            return dal.getAllVideos();
        }

        public string getVideoUrl(string id)
        {
            return dal.getVideoUrl(id).ToString();
        }

        public Video getVideo(int id)
        {
            return dal.getVideo(id);
        }

        /// <summary>
        /// 根据页数获取视频的id和名称
        /// </summary>
        /// <param name="pagesize">每页的数量</param>
        /// <param name="page">页码</param>
        /// <param name="total">总数</param>
        /// <returns>视频列表</returns>
        public List<Video> getAllVideo(int pagesize, int page, out int total)
        {
            List<Video> list = new List<Video>();
            DataSet ds = dal.GetList("id", pagesize, ((page - 1) * pagesize), "Video", "1=1", out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Video(){
                    id = Convert.ToInt32(row["id"]),
                    name = "" + row["name"]});
            }
            return list;
        }

        public bool addVideo(string name, string url, string url2)
        {
            return dal.addVideo(name, url,url2) > 0;
        }

        public bool updateVideo(int id, string name, string url, string url2)
        {
            return dal.updateVideo(id, name, url, url2) > 0;
        }

        public bool delVideo(int id)
        {
            return dal.delVideo(id) > 0;
        }
        #endregion

        #region 语录/公告表数据操作
        public DataSet getQuotations()
        {
            return dal.getQuotations();
        }

        public DataSet getAllQuotations()
        {
            return dal.getAllQuotations();
        }

        public DataSet getQuotation(int id)
        {
            return dal.getQuotation(id);
        }

        public bool addQuotations(string value, bool tips)
        {
            return dal.addQuotations(value, tips) > 0;
        }

        public bool updateQuotations(int id, string value, bool tips, DateTime cdate)
        {
            return dal.updateQuotations(id, value, tips, cdate) > 0;
        }

        public bool delQuotations(int id)
        {
            return dal.delQuotations(id) > 0;
        }
        #endregion

        #region 文章表数据操作
        public DataSet getArticle()
        {
            return dal.getArticle();
        }

        public DataSet getArticle(int count, int pageindex)
        {
            return dal.getArticle(count, pageindex);
        }

        public DataSet getArticle(int category, int count, int pageindex)
        {
            return dal.getArticle(category, count, pageindex);
        }

        public DataSet getArticle(int id, bool read)
        {
            if (read)
                dal.addArticleRead(id);
            return dal.getKnowledge(id);
        }

        public DataSet getRecycleArticle()
        {
            return dal.getRecycleArticle();
        }

        public bool addArticle(string title, int category, string artical, string user)
        {
            return dal.addArticle(title, category, artical, user) > 0;
        }

        public bool updateArticle(int id, int category, string title, string artical, string user)
        {
            return dal.updateArticle(id, category, title, artical, user) > 0;
        }

        public bool updateArticle(int id, bool valid)
        {
            return dal.updateArticle(id, valid) > 0;
        }

        public bool deleteArticle(int id)
        {
            return dal.updateArticle(id, false) > 0;
        }

        public bool restoreArticle(int id)
        {
            return dal.updateArticle(id, true) > 0;
        }

        public bool delArticle(int id)
        {
            return dal.deleteArticle(id) > 0;
        }

        public List<Article> getArticle(int pagesize, int page, out int total)
        {
            List<Article> list = new List<Article>();
            DataSet ds = dal.GetList("comment desc,edate desc", pagesize, ((page - 1) * pagesize), "Article", "valid=1", out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(
                    new Article()
                    {
                        id = Convert.ToInt32(row["id"]),
                        category = Convert.ToInt32(row["category"]),
                        title = row["title"].ToString(),
                        artical = "",
                        cdate = Convert.ToDateTime(row["cdate"]),
                        cuser = row["cuser"].ToString(),
                        edate = Convert.ToDateTime(row["edate"]),
                        euser = "" + row["euser"],
                        cread = Convert.ToInt32(row["cread"]),
                        comment = Convert.ToDateTime(row["comment"]),
                        valid = Convert.ToBoolean(row["valid"])
                    });
            }
            return list;
        }

        public List<Article> getArticle(int category, int pagesize, int page, out int total)
        {
            List<Article> list = new List<Article>();
            DataSet ds = dal.GetList("comment desc,edate desc", pagesize, ((page - 1) * pagesize), "Article", "category=" + category + " and valid=1", out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(
                    new Article()
                    {
                        id = Convert.ToInt32(row["id"]),
                        category = Convert.ToInt32(row["category"]),
                        title = row["title"].ToString(),
                        artical = "",
                        cdate = Convert.ToDateTime(row["cdate"]),
                        cuser = row["cuser"].ToString(),
                        edate = Convert.ToDateTime(row["edate"]),
                        euser = "" + row["euser"],
                        cread = Convert.ToInt32(row["cread"]),
                        comment = Convert.ToDateTime(row["comment"]),
                        valid = Convert.ToBoolean(row["valid"])
                    });
            }
            return list;
        }

        

        public Article getArticle(int id)
        {
            dal.addArticleRead(id);
            return dal.getArticle(id);
        }

        public Article getArticleNotRead(int id)
        {
            return dal.getArticle(id);
        }

        public bool addArticleRead(int id)
        {
            return dal.addArticleRead(id) > 0;
        }

        public bool addArticleComment(int id)
        {
            return dal.addArticleComment(id) > 0;
        }

        public bool clearArticle()
        {
            return dal.clearArticle() > 0;
        }
        #endregion

        #region 评论表数据操作
        public bool addComment(int aid, string uid, int star, string comment)
        {
            dal.addArticleComment(aid);
            return dal.addComment(aid, uid, star, comment) > 0;
        }

        public bool addComment(int aid, string uid, int star, string comment, string device)
        {
            dal.addArticleComment(aid);
            return dal.addComment(aid, uid, star, comment, device) > 0;
        }

        public bool addComment(int aid, string uid, string comment)
        {
            dal.addArticleComment(aid);
            return dal.addComment(aid, uid, 0, comment) > 0;
        }

        public bool addComment(int aid, string uid, string comment, string device)
        {
            dal.addArticleComment(aid);
            return dal.addComment(aid, uid, 0, comment, device) > 0;
        }

        public List<Comment> getComments(int aid,int pagesize, int page, out int total)
        {
            List<Comment> list = new List<Comment>();
            DataSet ds = dal.GetList("cdate desc", pagesize, ((page - 1) * pagesize), "Comment", "aid=" + aid, out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Comment()
                    {
                        id = Convert.ToInt32(row["id"]),
                        aid = Convert.ToInt32(row["aid"]),
                        uid = row["uid"].ToString(),
                        comment = show_content("" + row["comment"]),
                        cdate = Convert.ToDateTime(row["cdate"]),
                        device = "" + row["device"]
                    });
            }
            return list;
        }

        public DataSet getComment(int aid)
        {
            return dal.getComment(aid);
        }

        public bool deleteComment(int id)
        {
            return dal.deleteComment(id) > 0;
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
        #endregion

        #region 反馈表数据操作
        public DataSet getFeedback()
        {
            return dal.getFeedback();
        }
        public bool addFeedback(string feed, string contact, string device)
        {
            return dal.addFeedback(feed, device, contact) > 0;
        }

        public bool delFeedback(int id)
        {
            return dal.delFeedback(id) > 0;
        }
        #endregion

        #region 分类表数据操作
        public DataSet getCategory()
        {
            return dal.getCategory();
        }

        public List<Category> getCategorys()
        {
            return dal.getCategorys();
        }

        public Category getCategory(int id)
        {
            return dal.getCategory(id);
        }

        public bool addCategory(string name, string remark)
        {
            return dal.addCategory(name, remark) > 0;
        }

        public bool updateCategory(Category cate)
        {
            return dal.updateCategory(cate) > 0;
        }

        public bool delCategory(int id)
        {
            return dal.delCategory(id) > 0;
        }
        #endregion

        #region 版本下载表数据操作
        public DataSet getDownloadList()
        {
            return dal.getDownloadList();
        }

        public Download getDownloadList(int id)
        {
            return dal.getDownloadList(id);
        }

        public List<Download> getDownloadList(int pagesize, int page, out int total)
        {
            List<Download> list = new List<Download>();
            DataSet ds = dal.GetList("cdate desc", pagesize, ((page - 1) * pagesize), "Download", "1=1", out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(
                    new Download()
                    {
                        id = Convert.ToInt32(row["id"]),
                        version = row["ver"].ToString(),
                        url = row["url"].ToString(),
                        cdate = Convert.ToDateTime(row["cdate"])
                    });
            }
            return list;
        }

        public bool addDownload(string ver, string url, string cdate)
        {
            return dal.addDownload(ver, url, Convert.ToDateTime(cdate)) > 0;
        }

        public bool updateDownload(int id, string ver, string url, string cdate)
        {
            return dal.updateDownload(id, ver, url, Convert.ToDateTime(cdate)) > 0;
        }

        public bool delDownload(int id)
        {
            return dal.delDownload(id) > 0;
        }
        #endregion

        #region 资料下载表数据操作
        public DataSet getMaterialList()
        {
            return dal.getMaterialList();
        }

        public Material getMaterialList(int id)
        {
            return dal.getMaterialList(id);
        }

        public List<Material> getMaterialList(int pagesize, int page, out int total)
        {
            List<Material> list = new List<Material>();
            DataSet ds = dal.GetList("cdate desc", pagesize, ((page - 1) * pagesize), "Material", "1=1", out total);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(
                    new Material()
                    {
                        id = Convert.ToInt32(row["id"]),
                        title = row["title"].ToString(),
                        url = row["url"].ToString(),
                        psd = "" + row["psd"],
                        cdate = Convert.ToDateTime(row["cdate"]),
                        remark = "" + row["remark"]
                    });
            }
            return list;
        }

        public bool addMaterial(string title, string url, string psd, string remark)
        {
            return dal.addMaterial(title, url, psd, remark, 0) > 0;
        }

        public bool updateMaterial(int id, string title, string url, string psd, string remark)
        {
            return dal.updateMaterial(id, title, url, psd, remark) > 0;
        }

        public bool updateMaterialDowload(int id)
        {
            return dal.updateMaterialDowload(id) > 0;
        }

        public bool delMaterial(int id)
        {
            return dal.delMaterial(id) > 0;
        }
        #endregion
    }
}
