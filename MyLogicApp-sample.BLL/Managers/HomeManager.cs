using BpmTestProject.BLL.Repository.Workflow;
using MyLogicApp_sample.DAL;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLogicApp_sample.BLL.Managers
{
    public class HomeManager
    {
        private readonly MyContext _db;

        public HomeManager()
        {
            _db = new MyContext();
        }

        public List<Tuple<string, string, string, Guid>> GetTodoNotificationDescription(string userId)
        {
            var isAtamalar = _db.IsAtamalar.Where(x => x.AtananKullaniciId == userId && !x.TamamlandiMi);
            var semalar = _db.Semalar;
            var adimlar = _db.Adimlar;

            var notificationDescription = isAtamalar
                .Join(semalar, ia => ia.SemaId, s => s.Id, (ia, s) => new
                {
                    IsAtama = ia,
                    Sema = s
                }).
                Join(adimlar, ias => ias.IsAtama.SonAdimId, a => a.Id, (ias, a) => new { 
                    Support = ias,
                    YonlendirmeUrl = a.YonlendirmeUrl
                })
                .AsEnumerable()
                .Select(x => new Tuple<string, string, string, Guid>(x.Support.IsAtama.Aciklama, x.Support.Sema.Baslik, x.YonlendirmeUrl, x.Support.IsAtama.Id)).ToList();

            return notificationDescription;
        }
    }
}
