using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using SunShine.Model;

namespace SunShine.BLL {
    public class AdvertiseService {

        public static List<Advertise> GetALL() {
            TN db = new TN();
            return db.Advertises.ToList();
        }

        public static Advertise Get(string idadvertise) {
            TN db = new TN();
            return db.Advertises.Where(en => en.idadvertise == idadvertise).FirstOrDefault();
        }

        public static Advertise GetByCode(string code) {
            TN db = new TN();
            List<Advertise> entities = db.Advertises.Where(en => en.code == code).ToList().ToList();
            return entities.Count>0?entities.First():null;
        }

        public static Advertise Edit(Advertise advertise) {
            TN db = new TN();
            Advertise oldAdvertise = db.Advertises.Where(en => en.idadvertise == advertise.idadvertise).FirstOrDefault();

            oldAdvertise.idadvertise = advertise.idadvertise;
            oldAdvertise.code = advertise.code;
            oldAdvertise.title = advertise.title;
            
            db.SaveChanges();
            return oldAdvertise;
        }

        public static Advertise Add(Advertise advertise) {
            TN db = new TN();
            db.Advertises.Add(advertise);
            db.SaveChanges();
            return advertise;
        }
        
    }
}
