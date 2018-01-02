using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET.old
{
    public partial class VersionManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null && Session["LoginUser"] != null)
                {
                    if (((UserInfo)Session["LoginUser"]).Role == UserInfo.ROLE_ADMIN)
                    {
                        if (Request.QueryString["action"].Equals("add"))
                            btnAdd.Visible = true;
                        else if (Request.QueryString["action"].Equals("edt") && Request.QueryString["id"] != null)
                        {
                            getData(Convert.ToInt32(Request.QueryString["id"]));
                            btnEdit.Visible = true;
                        }
                    }
                }
                else
                    Response.Redirect("/Users/Login");
            }
        }

        private void getData(int id)
        {
            JavaDLL dll = new JavaDLL();
            Model.Download ver = dll.getDownloadList(id);
            if (ver != null)
            {
                txtVername.Text = ver.version;
                txtVerurl.Text = ver.url;
                txtVerdate.Text = ver.cdate.ToString("yyyy-MM-dd");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            JavaDLL dll = new JavaDLL();
            if (dll.addDownload(txtVername.Text.Trim(), txtVerurl.Text.Trim(), txtVerdate.Text.Trim()))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('添加失败！');", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            JavaDLL dll = new JavaDLL();
            if (dll.updateDownload(Convert.ToInt32(Request.QueryString["id"]), txtVername.Text.Trim(), txtVerurl.Text.Trim(), txtVerdate.Text.Trim()))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('修改失败！');", true);
        }
    }
}