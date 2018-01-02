using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET
{
    public partial class UserMng : System.Web.UI.Page
    {
        public String TmpUser
        {
            get
            {
                return ViewState["tmpuser"].ToString();
            }
            set
            {
                ViewState["tmpuser"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserInfo info = Session["LoginUser"] as UserInfo;
                if (info == null)
                    Response.Redirect("/Users/Login");
                if (info.Role == UserInfo.ROLE_ADMIN)
                {
                    TmpUser = info.UserName;
                    getData();
                }
                else
                    txtSearch.Visible = btnSearch.Visible = false;
            }
        }

        public void getData()
        {
            JavaDLL bll = new JavaDLL();
            DataSet ds = bll.searchAllUser(txtSearch.Text.Trim());
            gvUser.ShowFooter = (ds.Tables[0].Rows.Count <= gvUser.PageSize);
            gvUser.DataSource = ds;
            gvUser.DataBind();
        }

        public string getGender(object d)
        {
            if (Convert.ToBoolean(d))
                return "女";
            else
                return "男";
        }

        public bool getVisible(object e)
        {
            return !TmpUser.Equals(e);
        }

        public string getHead(string url)
        {
            if (url.Equals(string.Empty))
                return "/images/head.jpg";
            return url;
        }

        public string getRoles(int role)
        {
            switch (role)
            {
                case UserInfo.ROLE_ADMIN:
                    return "管理员";
                case UserInfo.ROLE_USER:
                    return "普通用户";
                case UserInfo.ROLE_VIP:
                    return "VIP用户";
                case UserInfo.ROLE_CONF:
                    return "受限用户";
            }
            return "-";
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            getData();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandName);
            if (TmpUser.Equals("admin"))
            {
                JavaDLL dll = new JavaDLL();
                if (dll.deleteUser(id))
                    getData();
                else
                    ClientScript.RegisterStartupScript(GetType(), "err", "alert('删除失败！');", true);
            }
            //else
            //ClientScript.RegisterStartupScript(GetType(), "err", "alert('你没有权限这么做！');", true);
        }

        protected void hidAjax_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getData();
        }
    }
}