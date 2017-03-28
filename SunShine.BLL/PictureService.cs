using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using SunShine.Model;

namespace SunShine.BLL
{
    public class PictureService
    {
        public static List<Picture> GetImagesByIdmodule(string idmodule,ModuleType moduleType)
        {
            TN db = new TN();
            return db.Pictures.Where(en => en.idmodule == idmodule&&en.moduletype==(int)moduleType).OrderBy(en => en.sortno).ToList();
        }

        public static List<Picture> AddMuti(List<Picture> images)
        {
            TN db = new TN();
            db.Pictures.AddRange(images);
            db.SaveChanges();
            return images;
        }

    }
}
