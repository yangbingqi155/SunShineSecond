using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunShine.EF;
using SunShine.Model;
using SunShine.BLL;

namespace SunShine.Web.Controllers
{
    public class ErrorController: Controller
    {
        public ActionResult Error404() {
             return View();

        }
    }
}