using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SunShine.Utils {
    public class SpecialStringHelper
    {
        #region 根据字符字数来拆分字符串
        /// <summary>
        /// 根据字符字数来拆分字符串
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<String> SplitStringByNum(string sourceStr, int num)
        {
            sourceStr = sourceStr.Trim();
            List<string> list = new List<string>();
            int mul = sourceStr.Length / num;
            int reminder = sourceStr.Length % num;
            int count = mul + (reminder == 0 ? 0 : 1);
            if (string.IsNullOrEmpty(sourceStr))
            {
                return list;
            }
            if (sourceStr.Length <= num)
            {
                list.Add(sourceStr);
                return list;
            }

            for (int i = 0; i < count; i++)
            {
                int subLength = num;
                if (i == count - 1 && reminder!=0)
                {
                    subLength = reminder;
                }
                list.Add(sourceStr.Substring(i * num, subLength));
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 对字符串进行检查和替换其中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
                    @"<script[^>]*?>.*?</script>",
                    @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                     @"<(.[^>]*)>",
                    @"([\r\n])[\s]+",
                    @"&(quot|#34);",
                    @"&(amp|#38);",
                    @"&(lt|#60);",
                    @"&(gt|#62);", 
                    @"&(nbsp|#160);", 
                    @"&(iexcl|#161);",
                    @"&(cent|#162);",
                    @"&(pound|#163);",
                    @"&(copy|#169);",
                    @"&#(\d+);",
                    @"-->",
                    @"<!--.*\n"
                    };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            //strOutput.Replace("\r\n", "");

            return strOutput;
        }
        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "'");
            theString = theString.Replace("\r\n", "<br> ");
            theString = theString.Replace("\n", "<br> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("'", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }
        /// <summary>
        /// 附加省略号
        /// </summary>
        /// <param name="length">截取的长度</param>
        /// <param name="isellipsis">是否追加省略号</param>
        /// <returns></returns>
        public static string Ellipsis(string input, int length, bool isellipsis)
        {
            input = input.Substring(0, input.Length >= length ? length : input.Length);
            return input;
        }
    }
}
