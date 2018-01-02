using Java21.Data;
using Java21.Logic;
using Java21.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Java21.NET.Controllers
{
    public class UsersController : Controller
    {
        private bool isLogin()
        {
            return Session["LoginUser"] != null;
        }

        public ActionResult Login()
        {
            if (isLogin())
                return RedirectToAction(UserAuth.resultAction, UserAuth.recontroller);
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserInfo user)
        {
            user.UserName = Request.Form["username"];
            user.UserPass = Request.Form["password"];
            JavaDLL dll = new JavaDLL();
            user = dll.Login(Request.Form["username"]);
            if (user == null || !user.Valid)
            {
                ViewData["LoginMsg"] = "用户不存在";
                return View(user);
            }
            if (user.Role == UserInfo.ROLE_CONF)
            {
                ViewData["LoginMsg"] = "账户已被限制使用";
                return View(user);
            }
            string s = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(Request.Form["password"]), "MD5");
            if (s.Equals(user.UserPass))
            {
                Session["LoginUser"] = user;
                return RedirectToAction(UserAuth.resultAction, UserAuth.recontroller);
            }
            else
            {
                ViewData["username"] = Request.Form["username"];
                ViewData["LoginMsg"] = "密码错误";
                return View(user);
            }
        }

        public ActionResult Details(string id)
        {
            JavaDLL dll = new JavaDLL();
            UserInfo info = dll.Login(id);
            if (info != null && info.Valid)
            {
                info.RoleStr = getRoles(info.Role);
                if (string.IsNullOrEmpty(info.HeadImg))
                    info.HeadImg = "/images/head.jpg";
                return View(info);
            }
            else
                return RedirectToAction("Home", "Java21");
        }

        public ActionResult SignIn()
        {
            if (isLogin())
                return RedirectToAction(UserAuth.resultAction, UserAuth.recontroller);
            ViewData["EmailEx"] = @"^(?:[a-z\d]+[_\-\+\.]?)*[a-z\d]+@(?:([a-z\d]+\-?)*[a-z\d]+\.)+([a-z]{2,})+$";
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserInfo user)
        {
            ViewData["username"] = user.UserName = Request.Form["username"].Trim();
            user.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(Request.Form["password"]), "MD5");
            ViewData["email"] = user.Email = Request.Form["email"].Trim();
            ViewData["nickName"] = user.NickName = Request.Form["nickName"];
            if (user.UserName.Length < 3)
            {
                ViewData["RegistMsg"] = "请输入用户名";
                return View(user);
            }
            if (user.UserPass.Length < 1)
            {
                ViewData["RegistMsg"] = "请输入密码";
                return View(user);
            }
            if (user.Email.Length <= 1)
            {
                ViewData["RegistMsg"] = "请输入邮箱";
                return View(user);
            }
            if (user.NickName.Length <= 0)
            {
                ViewData["RegistMsg"] = "请输入昵称";
                return View(user);
            }
            if (!IsUserName(user.UserName))
            {
                ViewData["RegistMsg"] = "用户名输入不正确";
                return View(user);
            }
            if (!IsEmail(user.Email))
            {
                ViewData["RegistMsg"] = "邮箱格式不正确";
                return View(user);
            }
            JavaDLL bll = new JavaDLL();
            if (!bll.checkUserforName(user.UserName))
            {
                ViewData["username"] = "";
                ViewData["RegistMsg"] = "用户名已存在，请重新输入";
                return View(user);
            }
            if (!bll.checkUserforEmail(user.Email))
            {
                ViewData["email"] = "";
                ViewData["RegistMsg"] = "邮箱已存在，请重新输入";
                return View(user);
            }
            user.Gender = Convert.ToInt32(Request.Form["gender"]);
            user.Mark = "他很懒，什么也没留下。";
            user.Role = UserInfo.ROLE_USER;
            user.Valid = true;
            user.HeadImg = "/images/head.jpg";
            if (bll.Register(user))
            {
                Log.e("Registed user:" + user.UserName + "_" + user.UserPass + "(" + Request.Form["password"] + ")(" + user.Email + ")", ConfigurationManager.AppSettings["logPath"] + "\\user_regist_" + DateTime.Now.ToString("yyyyMM") + ".log", DateTime.Now.ToString("MM-dd HH:mm:ss"));
                ViewData["username"] = user.UserName;
                Session["LoginUser"] = user;
                return RedirectToAction(UserAuth.resultAction, UserAuth.recontroller);
            }
            else
            {
                ViewData["RegistMsg"] = "注册失败！";
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Home", "Java21");
        }

        public ActionResult Setting(string er)
        {
            UserInfo info = (UserInfo)Session["LoginUser"];
            if (info != null)
            {
                if (er != null && er.Equals("error"))
                    ViewData.Add("portError", "请上传小于3MB的图片文件");
                else if (er != null && er.Equals("psd"))
                    ViewData.Add("psdSuc", "密码修改成功");
                return View(info);
            }
            else
                return Redirect("/users/login");
        }

        [HttpPost]
        public ActionResult Setting(UserInfo user)
        {
            user = (UserInfo)Session["LoginUser"];
            if (user == null)
                return RedirectToAction("Login", "Users");
            string path = Session["tempPath"].ToString();
            string src = Session["tempSrc"].ToString();
            int x = Convert.ToInt32(Request["x"]);
            int y = Convert.ToInt32(Request["y"]);
            int w = Convert.ToInt32(Request["w"]);
            int h = Convert.ToInt32(Request["h"]);
            string file = user.UserName + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(src);
            if (SaveImage(path + "\\" + src, path + "\\" + file, w, h, x, y))
            {
                user.HeadImg = "/portrait/" + file;
                JavaDLL dll = new JavaDLL();
                if (dll.updateUser(user))
                    Session["LoginUser"] = user;
                FileInfo s = new FileInfo(path + "\\" + src);
                if (s.Exists)
                    s.Delete();
                ViewData.Add("headimage", user.HeadImg);
            }
            Session.Remove("tempPath");
            Session.Remove("tempSrc");
            return RedirectToAction("Setting", "Users");
        }

        [HttpPost]
        public ActionResult Portrait()
        {
            string path = Request.PhysicalApplicationPath + "portrait";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            HttpPostedFileBase file = Request.Files["myfile"];
            string type = Path.GetExtension(file.FileName).ToLower();
            if ((type.Equals(".jpg") || type.Equals(".png") || type.Equals(".jpeg") || type.Equals(".gif")) && file.ContentLength <= 3145728)
            {
                Image img = Image.FromStream(file.InputStream);
                int max = img.Width < img.Height ? img.Width : img.Height;
                int maxPix = 200;//设定最大像素
                string src = "temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + type;
                if (max > maxPix)
                {
                    int width, height;
                    width = img.Width;
                    height = img.Height;
                    float scale = ((float)max / (float)maxPix);
                    if (width < height)
                    {
                        width = maxPix;
                        height = (int)(height / scale);
                    }
                    else
                    {
                        height = maxPix;
                        width = (int)(width / scale);
                    }
                    Image newImg = new Bitmap(img, width, height);
                    newImg.Save(path + "\\" + src, img.RawFormat);
                }
                else
                    file.SaveAs(path + "\\" + src);
                ViewData.Add("imgresouce", "/portrait/" + src);
                Session["tempPath"] = path;
                Session["tempSrc"] = src;
                return View();
            }
            else
                return RedirectToAction("Setting", "Users", new { er="error" });
        }

        [HttpPost]
        public ActionResult Settings()
        {
            UserInfo user = (UserInfo)Session["LoginUser"];
            if (user == null)
                return RedirectToAction("Login", "Users");
            user.NickName = Request["nickname"];
            user.Gender = Convert.ToInt32(Request["gender"]);
            user.QQ = Request["qq"];
            user.Mobile = Request["mobile"];
            user.Birthday = Convert.ToDateTime(Request["birth"]);
            user.SetAddress(Request["prov"],Request["city"],Request["dist"]);
            user.Mark = Request["remark"];
            JavaDLL dll = new JavaDLL();
            dll.updateFillUser(user);
            return RedirectToAction("users", "Java21");
        }

        public ActionResult ChangePassword()
        {
            if (!isLogin())
                return Redirect("/users/login");
            return View();
        }

        public string getRoles(int role)
        {
            switch (role)
            {
                case UserInfo.ROLE_ADMIN:
                    return "管理员";
                case UserInfo.ROLE_USER:
                    return "普通用户";
                case UserInfo.ROLE_VIP:
                    return "VIP用户";
                case UserInfo.ROLE_CONF:
                    return "受限用户";
            }
            return "-";
        }

        [HttpPost]
        public ActionResult ChangePassword(UserInfo user)
        {
            user = (UserInfo)Session["LoginUser"];
            if (user == null)
                return Redirect("/users/login");
            string psd = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(Request.Form["password"]), "MD5");
            if (!psd.Equals(user.UserPass))
            {
                ViewData.Add("PassMsg", "原密码输入不正确");
                return View();
            }
            string newp = Request["newpassword"];
            string yzp = Request["newpassword2"];
            if (!newp.Equals(yzp))
            {
                ViewData.Add("PassMsg", "两次密码输入不相同");
                return View();
            }
            if (psd.Equals(user.UserPass) && yzp.Length > 0)
            {
                user.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(Security.MD5Encrypt(yzp), "MD5");
                JavaDLL dll = new JavaDLL();
                if (dll.changePassword(user.ID, user.UserPass))
                    return RedirectToAction("Setting", "Users", new { er = "psd" });
                else
                {
                    ViewData.Add("PassMsg", "密码修改失败");
                    return View();
                }
            }
            return View(user);
        }

        public static bool IsUserName(string input)
        {
            string regex = @"^[A-Za-z0-9]+$";
            return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
        }

        public static bool IsEmail(string input)
        {
            //string regex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            string regex = @"^(?:[a-z\d]+[_\-\+\.]?)*[a-z\d]+@(?:([a-z\d]+\-?)*[a-z\d]+\.)+([a-z]{2,})+$";
            string[] strs = input.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                if (!Regex.IsMatch(strs[i], regex, RegexOptions.IgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        private void CropImage(string originaImgPath, int width, int height, int x, int y)
        {
            if (SaveImage(originaImgPath,"", width, height, x, y))
            {
                Response.Write("图像保存失败！");
            }
        }

        /// <summary>
        /// 剪裁图像
        /// </summary>
        /// <param name="Img"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private bool SaveImage(string img,string path, int width, int height, int x, int y)
        {
            try
            {
                using (var OriginalImage = new Bitmap(img))
                {
                    using (var bmp = new Bitmap(width, height, OriginalImage.PixelFormat))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);//裁剪图片
                            bmp.Save(path, OriginalImage.RawFormat);
                            return true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.e(Ex.Message);
                return false;
            }
        }
    }
}
