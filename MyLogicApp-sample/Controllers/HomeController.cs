using BpmTestProject.BLL.Repository.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLogicApp_sample.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public static int GetTodoCount(string userId)
        {
            var repo = new IsAtamaRepo();
            var count = repo.Queryable().Count(x => x.AtananKullaniciId == userId && !x.TamamlandiMi);
            return count;
        }
    }
}