using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SunShine.EF;

namespace SunShine.Model
{
    public class FriendURLViewModel:FriendURL
    {
        [Display(Name ="编号")]
        [StringLength(500)]
        public new string idurl { get; set; }

        [Display(Name = "链接地址")]
        [StringLength(500)]
        public new string url { get; set; }

        [Display(Name = "链接标题")]
        [StringLength(200)]
        public new string title { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        public void CopyFromBase(FriendURL friendURL)
        {
            this.idurl = friendURL.idurl;
            this.url = friendURL.url;
            this.title = friendURL.title;
            this.sortno = friendURL.sortno;

        }
        public void CopyToBase(FriendURL friendURL)
        {
            friendURL.idurl = this.idurl;
            friendURL.url = this.url;
            friendURL.title = this.title;
            friendURL.sortno = this.sortno;
        }
    }
}
