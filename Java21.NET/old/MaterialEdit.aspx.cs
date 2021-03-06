﻿using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET.old
{
    public partial class MaterialEdit : System.Web.UI.Page
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
            Model.Material cate = dll.getMaterialList(id);
            if (cate != null)
            {
                txtTitle.Text = cate.title;
                txtUrl.Text = cate.url;
                txtPassword.Text = cate.psd;
                txtRemark.Text = cate.remark;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('请输入标题');", true);
                return;
            }
            JavaDLL dll = new JavaDLL();
            if (dll.addMaterial(txtTitle.Text.Trim(), txtUrl.Text.Trim(), txtPassword.Text.Trim(), txtRemark.Text))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('添加失败！');", true);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim().Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('请输入标题');", true);
                return;
            }
            JavaDLL dll = new JavaDLL();
            if (dll.updateMaterial(Convert.ToInt32(Request.QueryString["id"]), txtTitle.Text, txtUrl.Text.Trim(), txtPassword.Text.Trim(), txtRemark.Text))
                ClientScript.RegisterStartupScript(GetType(), "err", "top.closeDialog()", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "err", "alert('修改失败！');", true);
        }
    }
}