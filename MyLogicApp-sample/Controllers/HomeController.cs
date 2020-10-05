using BpmTestProject.BLL.Repository.Workflow;
using Microsoft.AspNet.Identity;
using MyLogicApp_sample.BLL.Managers;
using MyLogicApp_sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLogicApp_sample.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly HomeManager _homeManager;

        public HomeController()
        {
            _homeManager = new HomeManager();
        }
        public ActionResult Index()
        {
            var model = new List<GorevViewModel>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.Identity.GetUserId();
                model = new IsAtamaRepo().Queryable()
                    .Where(x => x.AtananKullaniciId == userId)
                    .OrderBy(x => x.SonTarih)
                    .Select(x => new GorevViewModel
                    {
                        IsAtamaId = x.Id,
                        Baslik = x.Sema.Baslik,
                        Aciklama = x.Aciklama,
                        BaslangicTarihi = x.BaslangicTarihi,
                        SonTarih = x.SonTarih,
                        AdimSirasi = x.SonAdim.Sira,
                        ToplamAdim = x.Sema.AdimSayisi,
                        TamamlandiMi = x.TamamlandiMi,
                        YonlendirmeUrl = x.SonAdim.YonlendirmeUrl
                    })
                    .ToList();
            }

            return View(model);
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

        public static Tuple<string,string> GetTodoNotificationDescription(string userId)
        {
            var isAtamaRepo = new IsAtamaRepo().Queryable().Where(x => x.AtananKullaniciId == userId && !x.TamamlandiMi);
            var semaRepo = new SemaRepo().Queryable();

            var notificationDescription = isAtamaRepo
                .Join(semaRepo, ia => ia.SemaId, s => s.Id, (ia, s) => new
                {
                    IsAtama = ia,
                    Sema = s
                })
                .Select(x => new Tuple<string, string>(x.IsAtama.Aciklama, x.Sema.Baslik)).First();

            return notificationDescription;

        }

        public JsonResult GetNotificationDescription()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var notificationDescription = _homeManager.GetTodoNotificationDescription(userId);

            return Json(notificationDescription, JsonRequestBehavior.AllowGet);
        }
    }
}