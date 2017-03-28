using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SunShine.EF;
using SunShine.Model;

namespace SunShine.BLL {
    public class ManageUserService
    {
        public static List<ManageUser> GetALL() {
            TN tn = new TN();
            return tn.ManageUsers.ToList();
        }


        public static ManageUser GetManageUserByUserName(string userName) {
            return GetALL().Where(en => en.username == userName).FirstOrDefault();
        }

        public static ManageUser Get(string iduser) {
            TN tn = new TN();
            return tn.ManageUsers.Find(iduser);
        }

        public static ManageUser Add(ManageUser manageUser)
        {
            TN db = new TN();
            db.ManageUsers.Add(manageUser);
            db.SaveChanges();
            return manageUser;
        }

        public static ManageUser Edit(ManageUser manageUser)
        {
            TN db = new TN();
            ManageUser oldManageUser = db.ManageUsers.Where(en => en.iduser == manageUser.iduser).FirstOrDefault();

            oldManageUser.iduser = manageUser.iduser;
            oldManageUser.username = manageUser.username;
            oldManageUser.phone = manageUser.phone;
            oldManageUser.notes = manageUser.notes;
            oldManageUser.inuse = manageUser.inuse;
            
            db.SaveChanges();
            return oldManageUser;
        }

        public static ManageUser PasswordEdit(ManageUser manageUser) {
            TN db = new TN();
            ManageUser oldManageUser = db.ManageUsers.Where(en => en.iduser == manageUser.iduser).FirstOrDefault();

            oldManageUser.iduser = manageUser.iduser;
            oldManageUser.md5salt = manageUser.md5salt;
            oldManageUser.password = manageUser.password;

            db.SaveChanges();
            return oldManageUser;
        }

        public static List<ManageUser> SearchByUserName(string userName) {
            List<ManageUser> entities = new List<ManageUser>();
            TN db = new TN();
            entities = db.ManageUsers.Where(en => en.username.Contains(userName)).ToList();

            return entities;
        }
    }
}