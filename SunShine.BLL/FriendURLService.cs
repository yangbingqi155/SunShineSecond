using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.EF;
using SunShine.Model;
using SunShine.Utils;

namespace SunShine.BLL
{
    public class FriendURLService
    {

        public static List<FriendURL> GetALL()
        {
            TN db = new TN();
            return db.FriendURLs.OrderBy(en => en.sortno).ToList();
        }

        public static FriendURL Get(string idurl)
        {
            TN db = new TN();
            return db.FriendURLs.Where(en => en.idurl == idurl).FirstOrDefault();
        }

        public static FriendURL Edit(FriendURL friendURL)
        {
            TN db = new TN();
            FriendURL oldFriendURL = db.FriendURLs.Where(en => en.idurl == friendURL.idurl).FirstOrDefault();

            oldFriendURL.idurl = friendURL.idurl;
            oldFriendURL.url = friendURL.url;
            oldFriendURL.title = friendURL.title;
            oldFriendURL.sortno = friendURL.sortno;
           
            db.SaveChanges();
            return oldFriendURL;
        }



        public static FriendURL Add(FriendURL friendURL)
        {
            TN db = new TN();
            db.FriendURLs.Add(friendURL);
            db.SaveChanges();
            return friendURL;
        }

        public static bool Delete(List<string> idurls) {
            int count = 0;
            TN db = new TN();
            for (int i = 0; i < idurls.Count; i++) {
                FriendURL friendURL = db.FriendURLs.Remove(db.FriendURLs.Find(idurls[i]));
                if (friendURL != null) {
                    count++;
                }
            }
            db.SaveChanges();
            return count > 0 ? true : false;
        }
    }
}
