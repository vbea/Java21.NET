using System;
using System.Web.Mvc;
using System.Web.UI;
namespace Vbes.WebControls.Mvc
{
	public static class ScriptResourceExtensions
	{
		public static void RegisterMvcPagerScriptResource(this HtmlHelper html)
		{
			Page page = html.ViewContext.HttpContext.CurrentHandler as Page;
			string webResourceUrl = (page ?? new Page()).ClientScript.GetWebResourceUrl(typeof(PagerHelper), "Vbes.WebControls.Mvc.MvcPager.min.js");
			html.ViewContext.Writer.Write("<script type=\"text/javascript\" src=\"" + webResourceUrl + "\"></script>");
		}
	}
}
