using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET.old
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginUser"] == null)
                    Response.Redirect("/Users/Login");
                if ((Session["LoginUser"] as UserInfo).Role == UserInfo.ROLE_ADMIN)
                    getData();
            }
        }

        public void getData()
        {
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getDownloadList();
            gvList.ShowFooter = (ds.Tables[0].Rows.Count <= gvList.PageSize);
            gvList.DataSource = ds;
            gvList.DataBind();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandName);
            JavaDLL dll = new JavaDLL();
            if (dll.delDownload(id))
                getData();
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('删除失败！');", true);
        }

        protected void linkData_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            getData();
        }
    }
}