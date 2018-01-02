using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET.old
{
    public partial class JavaAudio : System.Web.UI.Page
    {
        string[] asymble = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    JavaDLL bll = new JavaDLL();
                    try
                    {
                        string u = bll.getVideoUrl(Request.QueryString["id"]);
                        javaVideo.InnerHtml = string.Format(url, u);
                    }
                    catch (Exception)
                    {
                        javaVideo.InnerHtml = "<div style=\"width:100%; text-align:left;\">Bad Request</div>";
                    }
                }
                else
                    javaVideo.InnerHtml = "<div style=\"width:100%; text-align:left;\">Bad Request</div>";
            }*/
        }

        protected void btnEncode_Click(object sender, EventArgs e)
        {
            string text = txtPiano.Text.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
            int length = text.Length;
            string[] codes = new string[length];
            bool bracket = false;
            for (int i = 0; i < length; i++)
            {
                string s = text.Substring(i, 1);
                if (s.Equals("("))
                {
                    bracket = true;
                    codes[i] = "(";
                    continue;
                }
                if (s.Equals(")"))
                {
                    bracket = false;
                    codes[i] = "),";
                    codes[i - 1] = codes[i - 1].Replace("-", "");
                    continue;
                }
                if (s.Equals("-"))
                {
                    codes[i] = "-1" + ",";
                    continue;
                }
                else
                {
                    if (bracket)
                        codes[i] = getIndex(s) + "-";
                    else
                        codes[i] = getIndex(s) + ",";
                }

            }
            txtValue.Text = string.Join("", codes).Trim(',').Replace("(", "").Replace(")", "");
        }

        private string getIndex(string asb)
        {
            for (int i = 0; i < asymble.Length; i++)
            {
                if (asb.ToUpper().Equals(asymble[i]))
                    return "" + i;
            }
            return asb;
        }

        private string getString(string[] index)
        {
            string str = "";
            foreach (string q in index)
            {
                str += getString(q);
            }
            return str;
        }

        private string getString(string index)
        {
            try
            {
                return asymble[Convert.ToInt32(index)];
            }
            catch
            {
                return index;
            }
        }

        protected void btnEycode_Click(object sender, EventArgs e)
        {
            string[] text = txtPiano.Text.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string value = "";
            for (int i = 0; i < text.Length; i++)
            {
                string sd = text[i];
                if (sd.Equals("-1"))
                {
                    value += "-";
                }
                else if (sd.Length > 2)
                {
                    string[] li = sd.Split('-');
                    if (li.Length > 1)
                    {
                        value += "(" + getString(li) + ")";
                    }
                }
                else
                    value += getString(sd);
            }
            txtValue.Text = value;
        }

        protected void btnNewAlphabet_Click(object sender, EventArgs e)
        {
            string text = txtPiano.Text.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
            int length = text.Length;
            List<string> codes = new List<string>();
            bool bracket = false;
            bool extend = false;
            string brack = "";
            string exte = "";
            for (int i = 0; i < length; i++)
            {
                string s = text.Substring(i, 1);
                if (s.Equals(","))
                    continue;
                if (s.Equals("["))
                {
                    extend = true;
                    continue;
                }
                if (s.Equals("]"))
                {
                    extend = false;
                    if (bracket)
                        brack += exte + "-";
                    else
                        codes.Add(exte);
                    exte = "";
                    continue;
                }
                if (s.Equals("("))
                {
                    bracket = true;
                    continue;
                }
                if (s.Equals(")"))
                {
                    codes.Add(brack.Trim('-'));
                    brack = "";
                    bracket = false;
                    continue;
                }
                else
                {
                    if (extend)
                    {
                        exte += s;
                        continue;
                    }
                    if (bracket)
                        brack += s + "-";
                    else
                        codes.Add(s);
                }
            }
            txtValue.Text = string.Join(",", codes);
        }

        protected void btnShowCode_Click(object sender, EventArgs e)
        {
            string[] sd;
            if (txtValue.Text.Trim().Equals(""))
                sd = txtPiano.Text.Trim().Split(',');
            else
                sd = txtValue.Text.Trim().Split(',');
            string s = "音节查看：<br />长度：" + sd.Length + "<br />";
            for (int i = 0; i < sd.Length; i++)
            {
                s += "<small>" + i + "</small> " + sd[i] + "<br />";
            }
            audioCode.InnerHtml = s;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPiano.Text = txtValue.Text = audioCode.InnerHtml = string.Empty;
        }

        protected void btnMobile_Click(object sender, EventArgs e)
        {
            Response.Redirect("JavaVideo.aspx");
        }
    }
}