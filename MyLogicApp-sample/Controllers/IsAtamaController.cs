using MyLogicApp_sample.BLL.Managers;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using MyLogicApp_sample.Web.Models;
using System.Web.Mvc;

namespace MyLogicApp_sample.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IsAtamaController : Controller
    {
        private readonly IsAtamaManager _isAtamaManager;

        public IsAtamaController()
        {
            _isAtamaManager = new IsAtamaManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SemalariGetir()
        {
            var semalar = _isAtamaManager.SemalariGetir();

            return Json(semalar, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalisanlariGetir()
        {
            var calisanlar = _isAtamaManager.CalisanlariGetir();

            return Json(calisanlar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AtamaKaydet(IsAtamaViewModel model)
        {
            var isAtama = new IsAtama
            {
                SemaId = model.SemaId,
                AtananKullaniciId = model.CalisanId,
                Aciklama = model.Aciklama,
                BaslangicTarihi = model.BaslangicTarihi,
                SonTarih = model.SonTarih
            };

            var result = _isAtamaManager.IsAtamaKaydet(isAtama) ? "Atama kaydedildi" : "Atama kaydedilemedi";

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}