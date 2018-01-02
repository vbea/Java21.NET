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
    public partial class CategoryExp : System.Web.UI.Page
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
            Model.Category cate = dll.getCategory(id);
            if (cate != null)
            {
                txtName.Text = cate.catename;
                txtValues.Text = cate.remark;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('请输入分类名称');", true);
                return;
            }
            JavaDLL dll = new JavaDLL();
            if (dll.addCategory(txtName.Text.Trim(),txtValues.Text.Trim()))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('添加失败！');", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('请输入分类名称');", true);
                return;
            }
            Model.Category cate = new Model.Category();
            cate.id = Convert.ToInt32(Request.QueryString["id"]);
            cate.catename = txtName.Text.Trim();
            cate.remark = txtValues.Text.Trim();
            JavaDLL dll = new JavaDLL();
            if (dll.updateCategory(cate))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('修改失败！');", true);
        }
    }
}