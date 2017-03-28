using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunShine.Model
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Display(Name = "所有")]
        ALL = 0,

        /// <summary>
        /// 产品
        /// </summary>
        [Display(Name ="产品")]
        Product=1,

        /// <summary>
        /// 文章
        /// </summary>
        [Display(Name = "文章")]
        Article = 10,

        /// <summary>
        /// 广告
        /// </summary>
        [Display(Name = "广告")]
        Advertise = 20,
    }
}