using MyLogicApp_sample.BLL.Account;
using MyLogicApp_sample.DAL;
using MyLogicApp_sample.ENTITY.Models.WorkFlow;
using System.Collections.Generic;
using System.Linq;

namespace MyLogicApp_sample.BLL.Managers
{
    public class IsAtamaManager
    {
        private readonly MyContext _db;
        public IsAtamaManager()
        {
            _db = _db ?? new MyContext();
        }

        public Dictionary<string, string> SemalariGetir()
        {
            var semalar = _db.Semalar
                .Select(x => new { x.Id, x.Baslik })
                .ToDictionary(x => x.Id.ToString(), x => x.Baslik);

            return semalar;
        }

        public Dictionary<string,string> CalisanlariGetir()
        {
            var calisanlar = MembershipTools.NewUserStore().Users
                .Select(x => new { id = x.Id, nameSurname = x.Name + " " + x.Surname })
                .ToDictionary(x => x.id, x => x.nameSurname);

            return calisanlar;
        }

        public bool IsAtamaKaydet(IsAtama isAtama)
        {
            isAtama.SonAdimId = _db.Baglayicilar.Where(x => x.SemaId == isAtama.SemaId)
                .Select(x => new { x.OncekiAdim.Id, x.OncekiAdim.Sira }).OrderBy(x => x.Sira).First().Id;
            _db.IsAtamalar.Add(isAtama);

            return _db.SaveChanges() > 0;
        }
    }
}
