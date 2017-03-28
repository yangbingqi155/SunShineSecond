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
    public class WebsiteInfoViewModel:WebSiteInfo
    {

        [Display(Name ="编号")]
        [StringLength(50)]
        public new string idsite { get; set; }

        [Display(Name = "网站名称")]
        [StringLength(500)]
        public new string sitename { get; set; }

        [Display(Name = "Copyright")]
        [StringLength(500)]
        public new string copyright { get; set; }

        [Display(Name = "地址")]
        [StringLength(500)]
        public new string address { get; set; }

        [Display(Name = "全国热线电话")]
        [StringLength(500)]
        public new string hotphoneallcountry { get; set; }

        [Display(Name = "免费咨询电话")]
        [StringLength(500)]
        public new string hotphone { get; set; }

        [Display(Name = "免费咨询电话2")]
        [StringLength(500)]
        public new string hotphone2 { get; set; }

        [Display(Name = "联系手机")]
        [StringLength(500)]
        public new string phone { get; set; }

        [Display(Name = "QQ")]
        [StringLength(500)]
        public new string qq { get; set; }

        [Display(Name = "QQ2")]
        [StringLength(500)]
        public new string qq2 { get; set; }


        public void CopyFromBase(WebSiteInfo websiteInfo)
        {
            this.idsite = websiteInfo.idsite;
            this.sitename = websiteInfo.sitename;
            this.copyright = websiteInfo.copyright;
            this.address = websiteInfo.address;
            this.hotphoneallcountry = websiteInfo.hotphoneallcountry;
            this.hotphone = websiteInfo.hotphone;
            this.hotphone2 = websiteInfo.hotphone2;
            this.phone = websiteInfo.phone;
            this.qq = websiteInfo.qq;
            this.qq2 = websiteInfo.qq2;
        }

        public void CopyToBase(WebSiteInfo websiteInfo)
        {
            websiteInfo.idsite = this.idsite;
            websiteInfo.sitename = this.sitename;
            websiteInfo.copyright = this.copyright;
            websiteInfo.address = this.address;
            websiteInfo.hotphoneallcountry = this.hotphoneallcountry;
            websiteInfo.hotphone = this.hotphone;
            websiteInfo.hotphone2 = this.hotphone2;
            websiteInfo.phone = this.phone;
            websiteInfo.qq = this.qq;
            websiteInfo.qq2 = this.qq2;
        }
    }
}
