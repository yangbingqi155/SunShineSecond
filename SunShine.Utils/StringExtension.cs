using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShine.Utils {
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension {
        
        public static string RemoveHTMLAndGetByNum(this string str,int num) {
            //str = str.Replace("<p>", "&&").Replace("</p>", "%%%");
            str = SpecialStringHelper.HtmlToTxt(str);
            //str = str.Replace("&&", "<p>").Replace("%%%", "</p>");
            if (string.IsNullOrEmpty(str)) { return ""; }
            return str.Length > num ? str.Substring(0, num)+"..." : str;
        }
    }
}
