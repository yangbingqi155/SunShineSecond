using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.EF;

namespace SunShine.Model
{
    [NotMapped]
    public class ManageUserViewModel : ManageUser
    {
        [Display(Name = "用户编号")]
        [StringLength(50)]
        public new string iduser { get; set; }

        [Display(Name = "用户名")]
        [StringLength(50)]
        [Required]
        public new string username { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(18)]
        public string ClearPassword { get; set; }

        [Display(Name = "新密码确认")]
        [Required]
        [StringLength(18)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(100)]
        public new string password { get; set; }

        [Display(Name = "MD5盐")]
        [StringLength(100)]
        [Required]
        public new string md5salt { get; set; }

        [Display(Name = "手机号")]
        [StringLength(50)]
        public new string phone { get; set; }

        [Display(Name = "备注")]
        [StringLength(500)]
        public new string notes { get; set; }

        [Required]
        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        [Required]
        [Display(Name ="原密码")]
        public string currentpassword { get; set; }

        [Required]
        [Display(Name = "新密码")]
        public string newpassword { get; set; }

        public void CopyFromBase(ManageUser user)
        {
            this.iduser = user.iduser;
            this.username = user.username;
            this.password = user.password;
            this.md5salt = user.md5salt;
            this.phone = user.phone;
            this.notes = user.notes;
            this.inuse = user.inuse;
        }

        public void CopyToBase(ManageUser user)
        {
            user.iduser = this.iduser;
            user.username = this.username;
            user.password = this.password;
            user.md5salt = this.md5salt;
            user.phone = this.phone;
            user.notes = this.notes;
            user.inuse = this.inuse;
        }
    }
}