﻿using Java21.Logic;
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
    public partial class Knowledges : System.Web.UI.Page
    {
        public string content = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null && Session["LoginUser"] != null)
                {
                    if (Request.QueryString["action"].Equals("add"))
                    {
                        getData();
                        labTitle.Text = "添加内容";
                        btnSave.CommandName = "add";
                        btnSave.Enabled = true;
                    }
                    else if (Request.QueryString["action"].Equals("edit") && Request.QueryString["id"] != null)
                    {
                        labTitle.Text = "编辑内容";
                        btnSave.CommandName = "edt";
                        getData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    linkAdvance.NavigateUrl = "Articles.aspx?" + Request.QueryString.ToString();
                }
                else
                    Response.Redirect("/Users/Login");
            }
        }

        //public string init()
        //{
        //    if (Request.QueryString["action"] !=  null)
        //    {
        //        if (Request.QueryString["action"].Equals("edit") && Request.QueryString["id"] != null)
        //            return getData());
        //    }
        //    return "";
        //}

        private void getData(int id)
        {
            JavaDLL bll = new JavaDLL();
            ddlCategory.DataSource = bll.getCategory();
            ddlCategory.DataTextField = "catename";
            ddlCategory.DataValueField = "id";
            ddlCategory.DataBind();
            DataSet ds = bll.getArticle(id, false);
            DataRow row = ds.Tables[0].Rows[0];
            ddlCategory.SelectedValue = row["category"].ToString();
            txtTitle.Text = row["title"].ToString();
            labTitle.Text += " - " + row["title"].ToString();
            content = row["artical"].ToString();
            labRemark.Text = "首次创建：" + row["cuser"] + "(" + Convert.ToDateTime(row["cdate"]).ToString("yyyy-MM-dd HH:mm:ss") + ")";
            if (!row["euser"].Equals(DBNull.Value))
                labRemark.Text += "，上次修改：" + row["euser"] + "(" + Convert.ToDateTime(row["edate"]).ToString("yyyy-MM-dd HH:mm:ss") + ")";
            UserInfo info = (UserInfo)Session["LoginUser"];
            if (row["cuser"].ToString().Equals(info.UserName) || info.Role == UserInfo.ROLE_ADMIN)
                btnSave.Enabled = true;
        }

        private void getData()
        {
            JavaDLL bll = new JavaDLL();
            ddlCategory.DataSource = bll.getCategory();
            ddlCategory.DataTextField = "catename";
            ddlCategory.DataValueField = "id";
            ddlCategory.DataBind();
        }

        public void add()
        {
            JavaDLL bll = new JavaDLL();
            if (bll.addArticle(txtTitle.Text.Trim(), Convert.ToInt32(ddlCategory.SelectedValue), content, ((UserInfo)Session["LoginUser"]).UserName))
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('新增成功！');window.location.href='Knowledge.aspx';", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('新增失败！');", true);
        }

        public void edit()
        {
            JavaDLL bll = new JavaDLL();
            if (bll.updateArticle(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(ddlCategory.SelectedValue), txtTitle.Text.Trim(), content, ((UserInfo)Session["LoginUser"]).UserName))
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('修改成功！');window.location.href='KnowledgeView.aspx?id=" + Request.QueryString["id"] + "';", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('修改失败！');", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            content = Request.Params["editorValue"];
            switch (((Button)sender).CommandName)
            {
                case "add":
                    add();
                    break;
                case "edt":
                    edit();
                    break;
            }

        }
    }
}