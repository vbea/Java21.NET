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
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginUser"] == null)
                    return;
                if ((Session["LoginUser"] as UserInfo).Role == UserInfo.ROLE_ADMIN)
                    getData();
            }
        }

        public void getData()
        {
            JavaDLL dll = new JavaDLL();
            DataSet ds = dll.getCategory();
            gvCategory.ShowFooter = (ds.Tables[0].Rows.Count <= gvCategory.PageSize);
            gvCategory.DataSource = ds;
            gvCategory.DataBind();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandName);
            JavaDLL dll = new JavaDLL();
            if (dll.delCategory(id))
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