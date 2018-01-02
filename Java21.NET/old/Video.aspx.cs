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
    public partial class Video : System.Web.UI.Page
    {
        //string url = "<p>{0}</p><embed src='{1}' quality='high' width='480' height='400' align='middle' allowFullScreen='true' quality='high' allowScriptAccess='always' type='application/x-shockwave-flash' />";
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

        private void getData()
        {
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getAllVideo();
            gvCategory.ShowFooter = (ds.Tables[0].Rows.Count <= gvCategory.PageSize);
            gvCategory.DataSource = ds;
            gvCategory.DataBind();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandName);
            JavaDLL dll = new JavaDLL();
            if (dll.delVideo(id))
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
            gvCategory.PageIndex = e.NewPageIndex;
            getData();
        }
    }
}