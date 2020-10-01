
using MyLogicApp_sample.BLL.Managers;
using MyLogicApp_sample.ENTITY.Models;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using MyLogicApp_sample.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace MyLogicApp_sample.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IsPlaniController : Controller
    {
        private readonly IsPlaniManager _isPlaniManager;
        public IsPlaniController()
        {
            _isPlaniManager = new IsPlaniManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SemaKaydet(SemaViewModel model)
        {
            var sema = new Sema
            {
                Baslik = model.Baslik,
                Aciklama = model.Aciklama
            };

            var adimlar = model.Adimlar
                .Select(x => new Adim
                {
                    Baslik = x.Baslik,
                    YonlendirmeUrl = x.YonlendirmeUrl
                }).ToList();

            var result = _isPlaniManager.SemaKaydet(sema, adimlar) ? "Şema kaydedildi" : "Şema kaydedilemedi";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}