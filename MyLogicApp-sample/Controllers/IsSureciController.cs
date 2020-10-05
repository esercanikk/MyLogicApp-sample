using BpmTestProject.BLL.Repository.Workflow;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using MyLogicApp_sample.BLL.Account;
using MyLogicApp_sample.Models;
using MyLogicApp_sample.SignalR;
using MyLogicApp_sample.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLogicApp_sample.Controllers
{
    public class IsSureciController : Controller
    {
        public ActionResult Index(Guid isAtamaId)
        {
            var gorevData = new IsAtamaRepo().Queryable().First(x => x.Id == isAtamaId);
            var gorev = new GorevViewModel
            {
                IsAtamaId = gorevData.Id,
                Baslik = gorevData.Sema.Baslik,
                Aciklama = gorevData.Aciklama,
                BaslangicTarihi = gorevData.BaslangicTarihi,
                SonTarih = gorevData.SonTarih,
                AdimSirasi = gorevData.SonAdim.Sira,
                ToplamAdim = gorevData.Sema.AdimSayisi,
                TamamlandiMi = gorevData.TamamlandiMi
            };

            var adim = new AdimViewModel
            {
                Baslik = gorevData.SonAdim.Baslik,
                YonlendirmeUrl = gorevData.SonAdim.YonlendirmeUrl,
                Sira = gorevData.SonAdim.Sira
            };

            var model = new IsSureciViewModel
            {
                Gorev = gorev,
                Adim = adim
            };

            return View(model);
        }
        [HttpPost]
        public JsonResult AdimTamamla(Guid id, string aciklama)
        {
            var isAtamaRepo = new IsAtamaRepo();
            var baglayiciRepo = new BaglayiciRepo();
            var isAtama = isAtamaRepo.Queryable().First(x => x.Id == id);
            var baglayici = baglayiciRepo.Queryable().FirstOrDefault(x => x.SemaId == isAtama.SemaId && x.Id == isAtama.SonAdimId);
            if (baglayici == null)
            {
                isAtama.TamamlandiMi = true;
                if (isAtamaRepo.Update() > 0)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<WorkflowHub>();
                    var user = MembershipTools.NewUserManager().FindById(isAtama.AtananKullaniciId);
                    hubContext.Clients.User(user.UserName).endCurrentTask();
                    return Json(true);
                }
                else
                    return Json(false);
            }
            isAtama.SonAdimId = baglayici.Id2;
            if (!string.IsNullOrEmpty(aciklama)) isAtama.Sonuc = aciklama;
            var result = isAtamaRepo.Update() > 0;

            return Json(result);
        }

        [HttpPost]
        public JsonResult AdimDataKaydet(Guid id, string sonucData)
        {
            var isAtamaRepo = new IsAtamaRepo();
            var baglayiciRepo = new BaglayiciRepo();
            var isAtama = isAtamaRepo.Queryable().First(x => x.Id == id);
            var baglayici = baglayiciRepo.Queryable().FirstOrDefault(x => x.SemaId == isAtama.SemaId && x.Id == isAtama.SonAdimId);
            if (baglayici == null)
            {
                isAtama.TamamlandiMi = true;
                if (isAtamaRepo.Update() > 0)
                {
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<WorkflowHub>();
                    var user = MembershipTools.NewUserManager().FindById(isAtama.AtananKullaniciId);
                    hubContext.Clients.User(user.UserName).endCurrentTask();
                    return Json(true);
                }
                else
                    return Json(false);
            }
            isAtama.SonAdimId = baglayici.Id2;
            isAtama.SonucDataJson = sonucData;
            var result = isAtamaRepo.Update() > 0;

            return Json(result);
        }

        public ActionResult OrnekForm()
        {
            return View();
        }

        [HttpPost]
        public JsonResult OrnekForm(string ad, string soyad)
        {
            return Json(ad + " " + soyad);
        }
    }
}