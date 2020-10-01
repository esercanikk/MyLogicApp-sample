using MyLogicApp_sample.DAL;
using MyLogicApp_sample.ENTITY.Models;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using System.Collections.Generic;

namespace MyLogicApp_sample.BLL.Managers
{
    public class IsPlaniManager
    {
        private readonly MyContext _db;
        public IsPlaniManager()
        {
            _db = _db ?? new MyContext();
        }

        public bool SemaKaydet(Sema sema, List<Adim> adimlar)
        {
            sema.AdimSayisi = adimlar.Count;
            _db.Semalar.Add(sema);

            for (int i = 0; i < adimlar.Count; i++)
            {
                adimlar[i].Sira = i + 1;
                _db.Adimlar.Add(adimlar[i]);

                if(i > 0)
                {
                    var baglayici = new Baglayici
                    {
                        Id = adimlar[i - 1].Id,
                        Id2 = adimlar[i].Id,
                        SemaId = sema.Id
                    };
                    _db.Baglayicilar.Add(baglayici);
                }
            }

            return _db.SaveChanges() > 0;
        }
    }
}
