using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vbes.WebControls.Mvc;

namespace Java21.NET.Controllers
{
    public class MaterialController : Controller
    {
        public ActionResult Material(int id)
        {
            if (id == 0)
                return RedirectToAction("Material", "Java21");
            JavaDLL dll = new JavaDLL();
            Model.Material model = dll.getMaterialList(id);
            UserInfo info = (UserInfo)Session["LoginUser"];
            if (info == null || info.Role == UserInfo.ROLE_USER)
                model.psd = "****";
            else
                dll.updateMaterialDowload(id);
            return View(model);
        }
    }
}
