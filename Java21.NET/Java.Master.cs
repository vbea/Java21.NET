using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Java21.Model;

namespace Java21.NET
{
    public partial class Java : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserInfo info = (UserInfo)Session["LoginUser"];
                if (info == null)
                {
                    divLogin.Visible =
                    listVideo.Visible =
                    listCate.Visible =
                    listVers.Visible =
                    listUser.Visible =
                    listFeed.Visible =
                    listSett.Visible =
                    listServ.Visible =
                    lisMater.Visible =
                    listQuot.Visible = false;
                    panSingup.Visible = true;
                    return;
                }
                else if (info.Role == UserInfo.ROLE_CONF)
                    Response.Redirect("/Users/Login");
                if (info.Role != UserInfo.ROLE_ADMIN)
                {
                    listCate.Visible =
                    listVideo.Visible =
                    listVers.Visible =
                    listUser.Visible =
                    listQuot.Visible =
                    listServ.Visible =
                    lisMater.Visible =
                    listFeed.Visible = false;
                }
                labUser.Text = info.NickName;
            }
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("/home.html");
        }
    }
}