using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using SunShine.Model;

namespace SunShine.BLL {
    public class SEOService {

        public static List<SEO> GetALL() {
            TN db = new TN();
            return db.SEOs.ToList();
        }

        public static SEO Get(string idseo) {
            TN db = new TN();
            return db.SEOs.Where(en => en.idseo == idseo).FirstOrDefault();
        }

        public static SEO GetByCode(string code) {
            TN db = new TN();
            return db.SEOs.Where(en => en.code == code).FirstOrDefault();
        }

        public static SEO Edit(SEO seo) {
            TN db = new TN();
            SEO oldSEO = db.SEOs.Where(en => en.idseo == seo.idseo).FirstOrDefault();

            oldSEO.idseo = seo.idseo;
            //oldSEO.code = seo.code;
            oldSEO.name = seo.name;
            oldSEO.seotitle = seo.seotitle;
            oldSEO.seokeywords = seo.seokeywords;
            oldSEO.seodescription = seo.seodescription;

            db.SaveChanges();
            return oldSEO;
        }

        public static WebSiteInfo Add(WebSiteInfo websiteInfo) {
            TN db = new TN();
            db.WebSiteInfoes.Add(websiteInfo);
            db.SaveChanges();
            return websiteInfo;
        }
    }
}
