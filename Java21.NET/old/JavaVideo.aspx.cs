using Java21.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Java21.NET
{
    public partial class JavaVideo : System.Web.UI.Page
    {
        string url = "<embed src='{0}' quality='high' width='480' height='320' align='middle'  type='application/x-shockwave-flash' />";
        string[] pinoAsy = {
            "A-3", "#A-3", "B-3", "C-2", "#C-2", "D-2", "#D-2", "E-2", "F-2", "#F-2", "G-2", "#G-2", "A-2", "#A-2", "B-2", "C-1", "#C-1", "D-1", "#D-1", "E-1", "F-1", "#F-1", "G-1", "#G-1", "A-1", "#A-1", "B-1", "C", "#C", "D", "#D", "E", "F", "#F", "G", "#G", "A", "#A", "B", "C1", "#C1", "D1", "#D1", "E1", "F1", "#F1", "G1", "#G1", "A1", "#A1", "B1", "C2", "#C2", "D2", "#D2", "E2", "F2", "#F2", "G2", "#G2", "A2", "#A2", "B2", "C3", "#C3", "D3", "#D3", "E3", "F3", "#F3", "G3", "#G3", "A3", "#A3", "B3", "C4", "#C4", "D4", "#D4", "E4", "F4", "#F4", "G4", "#G4", "A4", "#A4", "B4", "C5",
            "a-3", "#a-3", "b-3", "c-2", "#c-2", "d-2", "#d-2", "e-2", "f-2", "#f-2", "g-2", "#g-2", "a-2", "#a-2", "b-2", "c-1", "#c-1", "d-1", "#d-1", "e-1", "f-1", "#f-1", "g-1", "#g-1", "a-1", "#a-1", "b-1", "c", "#c", "d", "#d", "e", "f", "#f", "g", "#g", "a", "#a", "b", "c1", "#c1", "d1", "#d1", "e1", "f1", "#f1", "g1", "#g1", "a1", "#a1", "b1", "c2", "#c2", "d2", "#d2", "e2", "f2", "#f2", "g2", "#g2", "a2", "#a2", "b2", "c3", "#c3", "d3", "#d3", "e3", "f3", "#f3", "g3", "#g3", "a3", "#a3", "b3", "c4", "#c4", "d4", "#d4", "e4", "f4", "#f4", "g4", "#g4", "a4", "#a4", "b4", "c5" };
        string[] asymble = {
            "A1", "C1", "A2", "A3", "C2", "A4", "C3", "A5", "A6", "C4", "A7", "C5", "A8", "C6", "A9", "A10", "C7", "A11", "C8", "A12", "A13", "C9", "A14", "C10", "A15", "C11", "A16", "A", "C12", "B", "C13", "C", "D", "C14", "E", "C15", "F", "C16", "G", "H", "C17", "I", "C18", "J", "K", "C19", "L", "C20", "M", "C21", "N", "O", "C22", "P", "C23", "Q", "R", "C24", "S", "C25", "T", "C26", "U", "V", "C27", "W", "C28", "X", "Y", "C29", "Z", "C30", "B1", "C31", "B2", "B3", "C32", "B4", "C33", "B5", "B6", "C34", "B7", "C35", "B8", "C36", "B9", "B10",
            "a1", "c1", "a2", "a3", "c2", "a4", "c3", "a5", "a6", "c4", "a7", "c5", "a8", "c6", "a9", "a10", "c7", "a11", "c8", "a12", "a13", "c9", "a14", "c10", "a15", "c11", "a16", "a", "c12", "b", "c13", "c", "d", "c14", "e", "c15", "f", "c16", "g", "h", "c17", "i", "c18", "j", "k", "c19", "l", "c20", "m", "c21", "n", "o", "c22", "p", "c23", "q", "r", "c24", "s", "c25", "t", "c26", "u", "v", "c27", "w", "c28", "x", "y", "c29", "z", "c30", "b1", "c31", "b2", "b3", "c32", "b4", "c33", "b5", "b6", "c34", "b7", "c35", "b8", "c36", "b9", "b10"};
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("\"" + string.Join("\", \"", pinoAsy).ToUpper() + "\"");
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
            string text = txtPiano.Text.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "").ToUpper();
            string result = "";
            string[] enters = text.Split(';');
            for (int i = 0; i < enters.Length; i++)
            {
                string[] src = enters[i].Split(',');
                foreach (string _s in src)
                {
                    result += toHexMusic(_s);
                }
                result += "\n";
            }
            txtValue.Text = result;
        }

        public string toHexMusic(string sd)
        {
            string result = "";
            if (sd.IndexOf("U") == 0)
            {
                if (sd.IndexOf("UT") == 0)
                    sd = sd.Replace("UT", "--");
                else
                    sd = sd.Replace("U", "_");
                result += sd;
            }
            else if (sd.IndexOf("T") == 0)
            {
                if (sd.IndexOf("TU") == 0)
                    sd = sd.Replace("TU", "--");
                else
                    sd = sd.Replace("T", "__");
                result += sd;
            }
            else if (sd.IndexOf("W") == 0)
            {
                sd = sd.Replace("W", "_");
                result += sd;
            }
            else if (sd.StartsWith("("))
            {
                string hou = "";
                if (sd.IndexOf("[KL]") > 0)
                    hou = "-";
                if (sd.IndexOf("[K]") > 0)
                    hou = "_";
                if (sd.IndexOf("[L]") > 0)
                    hou = "";
                if (sd.IndexOf("[J]") > 0)
                    hou = "-_-";
                if (sd.IndexOf("[JL]") > 0)
                    hou = "___";
                if (sd.IndexOf("[JK]") > 0)
                    hou = "--";
                sd = getRegKuotex(sd);
                string[] sdf = null;
                if (sd.IndexOf(".") > 0)
                    sdf = sd.Split('.');
                else if (sd.IndexOf("@") > 0)
                {
                    sdf = sd.Split('@');
                    hou += "@)";
                }
                else if (sd.IndexOf("~") > 0)
                    sdf = sd.Split('~');
                else
                    sdf = new string[] { sd };
                if (sdf != null)
                {
                    string res = "(";
                    foreach (string _ds in sdf)
                    {
                        res += toHexMusic(_ds);
                        /*int index = getPianoIndex(_ds);
                        if (index != -1)
                            res += getAlphabet(index);
                        else
                            res += "<" + _ds + ">";*/
                    }
                    if (!hou.EndsWith(")"))
                        res += ")";
                    result += res + hou;
                }
            }
            else if (sd.IndexOf("<") >= 0)
            {
                string hou = "";
                if (sd.IndexOf("[KL]") > 0)
                    hou = "-";
                if (sd.IndexOf("[K]") > 0)
                    hou = "_";
                if (sd.IndexOf("[L]") > 0)
                    hou = "";
                if (sd.IndexOf("[N]") > 0)
                    hou = "";
                sd = getReGtex(sd);
                string[] sdf = null;
                if (sd.IndexOf(".") > 0)
                    sdf = sd.Split('.');
                else if (sd.IndexOf("@") > 0)
                    sdf = sd.Split('@');
                else
                    sdf = new string[] { sd };
                if (sdf != null)
                {
                    string res = "<";
                    foreach (string _s in sdf)
                    {
                        string _ds = _s;
                        if (_ds.EndsWith("[N]"))
                            _ds = _s.Replace("[N]", "");
                        if (_ds.StartsWith("("))
                            res += "(" + toHexMusic(_ds.Substring(1));
                        else if (_ds.EndsWith(")"))
                            res += toHexMusic(_ds.Substring(0, _ds.Length - 1)) + ")";
                        else
                            res += toHexMusic(_ds);
                        /*int index = getPianoIndex(_ds);
                        if (index != -1)
                            res += getAlphabet(index);
                        else
                            res += "<" + _ds + ">";*/
                    }
                    res += ">";
                    result += res + hou;
                }
            }
            else if (sd.IndexOf("[I]") > 0)
            {
                sd = sd.Replace("[I]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index).ToLower();
                else
                    result += "<" + sd + ">";
            }
            else if (sd.IndexOf("[L]") > 0)
            {
                sd = sd.Replace("[L]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index);
                else
                    result += "<" + sd + ">";
            }
            else if (sd.IndexOf("[LM]") > 0)
            {
                sd = sd.Replace("[LM]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "_";
                else
                    result += "<" + sd + ">";
            }
            else if (sd.IndexOf("[KL]") > 0)
            {
                sd = sd.Replace("[KL]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "-";
                else
                    result += "<" + sd + ">-";
            }
            else if (sd.IndexOf("[J]") > 0)
            {
                sd = sd.Replace("[J]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "-_-";
                else
                    result += "<" + sd + ">";
            }
            else if (sd.IndexOf("[JK]") > 0)
            {
                sd = sd.Replace("[JK]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "--";
                else
                    result += "<" + sd + ">--";
            }
            else if (sd.IndexOf("[JL]") > 0)
            {
                sd = sd.Replace("[JL]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "___";
                else
                    result += "<" + sd + ">___";
            }
            else if (sd.IndexOf("[JKL]") > 0)
            {
                sd = sd.Replace("[JKL]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "---";
                else
                    result += "<" + sd + ">---";
            }
            else if (sd.IndexOf("[JLK]") > 0)
            {
                sd = sd.Replace("[JLK]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index) + "---";
                else
                    result += "<" + sd + ">---";
            } 
            else if (sd.IndexOf("[K]") > 0)
            {
                sd = sd.Replace("[K]", "");
                result += getAlphabet(getPianoIndex(sd)) + "_";
            }
            else if (sd.IndexOf("[M]") > 0)
            {
                sd = sd.Replace("[M]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index).ToLower();
                else
                    result += "<" + sd + ">";
            }
            else if (sd.IndexOf("[N]") >= 0)
            {
                sd = sd.Replace("[N]", "");
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index).ToLower();
                else
                    result += "<" + sd + ">";
            }
            else
            {
                int index = getPianoIndex(sd);
                if (index != -1)
                    result += getAlphabet(index);
                else
                    result += "<" + sd + ">";
            }
            return result;
        }

        public int getPianoIndex(string s)
        {
            for (int i = 0; i < pinoAsy.Length; i++)
            {
                if (pinoAsy[i].Equals(s))
                    return i;
            }
            return -1;
        }

        public string getAlphabet(int index)
        {
            if (index >= 0)
            {
                string res = asymble[index];
                if (res.Length > 1)
                    return "[" + res + "]";
                else
                    return res;
            }
            return "";
        }

        public string getAlphabet1(int index)
        {
            if (index >= 0)
                return asymble[index];
            return "";
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

        private int getAsyIndex(string asb)
        {
            for (int i = 0; i < asymble.Length; i++)
            {
                if (asb.ToUpper().Equals(asymble[i]))
                    return i;
            }
            return -1;
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

        public string getRegKuotex(string str)
        {
            string pattern = @"\(.*?\)";//匹配模式
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match matche = regex.Match(str);
            return matche.Value.Trim('(', ')');
        }

        public string getReGtex(string str)
        {
            string pattern = @"\<.*?\>";//匹配模式
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match matche = regex.Match(str);
            return matche.Value.Trim('<', '>');
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

        protected void btnBass_Click(object sender, EventArgs e)
        {
            string text = txtPiano.Text.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");
            string[] src = text.Split(',');
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i].Length >= 3 && src[i].IndexOf("-") > 0)
                {
                    string[] mix = src[i].Split('-');
                    for (int j = 0; j < mix.Length; j++)
                    {
                        int ind = getAsyIndex(mix[j]);
                        if (ind > -1)
                            mix[j] = getAlphabet1(ind - 7);
                    }
                    src[i] = string.Join("-", mix);
                }
                else
                {
                    int index = getAsyIndex(src[i]);
                    if (index > -1)
                        src[i] = getAlphabet1(index - 7);
                }
            }
            txtValue.Text = string.Join(",", src);
        }

        protected void btnUpper_Click(object sender, EventArgs e)
        {
            txtValue.Text = txtPiano.Text.ToUpper();
        }

        protected void btnFixComma_Click(object sender, EventArgs e)
        {
            string text = txtPiano.Text.Trim();
            StringBuilder res = new StringBuilder();
            bool comma = false;
            for (int i = 0; i < text.Length; i++)
            {
                string s = text.Substring(i, 1);
                if (s.Equals("<"))
                    comma = true;
                if (s.Equals(">"))
                    comma = false;
                if (s.Equals(","))
                {
                    res.Append(comma ? "." : ",");
                    continue;
                }
                res.Append(s);
            }
            txtValue.Text = res.ToString();
        }
    }
}