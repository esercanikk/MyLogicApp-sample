using BpmTestProject.BLL.Repository.Workflow;
using Microsoft.AspNet.Identity;
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
                        TamamlandiMi = x.TamamlandiMi
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
    }
}