using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace SunShine.Web.Utils {
    public static class UrlHelperExtension {

        public static string PagerHTML(this UrlHelper url, int pageCount, int pageIndex, string actionName, string controllerName, RouteValueDictionary routeValues, int showMaxPageCount = 10) {

            StringBuilder sb = new StringBuilder();
            if (pageCount == 0) {
                throw new Exception("总页数不能为0.");
            }
            if (pageCount == 1) {
                return "";
            }

            if (routeValues == null && routeValues.Count > 0) {
                string key = routeValues.Keys.Where(en => en == "pageIndex").FirstOrDefault();
                if (string.IsNullOrEmpty(key)) {
                    routeValues.Add("pageIndex", pageIndex);
                }
            }

            sb.AppendLine("<nav>");
            sb.AppendLine("<ul class=\"pager\">");
            routeValues["pageIndex"] = 0;
            sb.AppendLine(string.Format("<li {2}><a href = \"{0}\" > {1} </a></li>", (pageIndex == 0) ? "javascript:void(0);" : url.Action(actionName, controllerName, routeValues), "首页", (pageIndex == 0) ? " class=\"active disabled\"" : ""));
            sb.AppendLine("<li [pre]>");
            routeValues["pageIndex"] = pageIndex - 1;
            sb.AppendLine(string.Format("<a href = \"{0}\" aria-label=\"Previous\">", pageIndex == 0 ? "javascript:void(0);" : url.Action(actionName, controllerName, routeValues)));
            sb.AppendLine("<span aria-hidden=\"true\">上一页</span>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            int j = 0;
            int half = showMaxPageCount / 2;
            int startIndex = showMaxPageCount > pageCount ? 0 : pageIndex;
            int endIndex = pageCount - pageIndex > showMaxPageCount ? pageIndex + showMaxPageCount : 0;
            if (pageCount - pageIndex > showMaxPageCount) {
                endIndex = pageIndex + showMaxPageCount - 1;
            }
            else {
                endIndex = pageCount - 1;
                startIndex = pageIndex - (showMaxPageCount - (endIndex - pageIndex));
            }

            for (int i = 0; i < pageCount; i++) {
                if ((i >= startIndex && i <= endIndex)) {
                    routeValues["pageIndex"] = i;
                    sb.AppendLine(string.Format("<li {2}><a href = \"{0}\" > {1} </a></li>", (pageIndex == i) ? "javascript:void(0);" : url.Action(actionName, controllerName, routeValues), i + 1, (pageIndex == i) ? " class=\"active disabled\"" : ""));
                }
                else {
                    if (j == 0 && i > endIndex) {
                        sb.AppendLine("<li class=\"active\"><span>...</span></li>");
                        j++;
                    }
                }
            }



            sb.AppendLine("<li [next]>");
            routeValues["pageIndex"] = pageIndex + 1;
            sb.AppendLine(string.Format("<a href=\"{0}\" aria-label=\"Next\">", (pageIndex == pageCount - 1) ? "javascript:void(0);" : url.Action(actionName, controllerName, routeValues)));
            sb.AppendLine("<span aria-hidden=\"true\">下一页</span>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            routeValues["pageIndex"] = pageCount - 1;
            sb.AppendLine(string.Format("<li {2}><a href = \"{0}\" > {1} </a></li>", (pageIndex == pageCount - 1) ? "javascript:void(0);" : url.Action(actionName, controllerName, routeValues), "末页", (pageIndex == pageCount - 1) ? " class=\"active disabled\"" : ""));
            sb.AppendLine("</ul>");
            sb.AppendLine("</nav>");

            sb = (pageIndex == 0) ? sb.Replace("[pre]", "class=\"disabled\"") : sb.Replace("[pre]", "");
            sb = (pageIndex == pageCount - 1) ? sb.Replace("[next]", "class=\"disabled\"") : sb.Replace("[next]", "");

            //把roteValue 重新初始化
            routeValues["pageIndex"] = 0;
            RouteValueDictionary dic = new RouteValueDictionary();
            dic.Add("action", routeValues["action"]);
            dic.Add("controller", routeValues["controller"]);

            routeValues.Clear();

            foreach (var item in dic)
            {
                routeValues.Add(item.Key,item.Value);
            }

            return sb.ToString();
        }

        public static string BootstrapPagerHTML(this UrlHelper url, int pageCount, int pageIndex, string actionName, string controllerName, RouteValueDictionary routeValues) {
            StringBuilder sb = new StringBuilder();
            if (pageCount == 0) {
                throw new Exception("总页数不能为0.");
            }
            if (pageCount == 1) {
                return "";
            }

            if (routeValues == null && routeValues.Count > 0) {
                string key = routeValues.Keys.Where(en => en == "pageIndex").FirstOrDefault();
                if (string.IsNullOrEmpty(key)) {
                    routeValues.Add("pageIndex", pageIndex);
                }
            }

            sb.AppendLine("<nav>");
            sb.AppendLine("<ul class=\"pagination\">");
            sb.AppendLine("<li [pre]>");
            routeValues["pageIndex"] = pageIndex - 1;
            sb.AppendLine(string.Format("<a href = \"{0}\" aria-label=\"Previous\">", pageIndex == 0 ? "" : url.Action(actionName, controllerName, routeValues)));
            sb.AppendLine("<span aria-hidden=\"true\">&laquo;</span>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            for (int i = 0; i < pageCount; i++) {
                routeValues["pageIndex"] = i;
                sb.AppendLine(string.Format("<li {2}><a href = \"{0}\" > {1} </a></li>", url.Action(actionName, controllerName, routeValues), i + 1, (pageIndex == i) ? " class=\"active\"" : ""));
            }

            sb.AppendLine("<li [next]>");
            routeValues["pageIndex"] = pageIndex + 1;
            sb.AppendLine(string.Format("<a href=\"{0}\" aria-label=\"Next\">", (pageIndex == pageCount - 1) ? "" : url.Action(actionName, controllerName, routeValues)));
            sb.AppendLine("<span aria-hidden=\"true\">&raquo;</span>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("</nav>");

            sb = (pageIndex == 0) ? sb.Replace("[pre]", "class=\"disabled\"") : sb.Replace("[pre]", "");
            sb = (pageIndex == pageCount - 1) ? sb.Replace("[next]", "class=\"disabled\"") : sb.Replace("[next]", "");

            //把roteValue 重新初始化
            routeValues["pageIndex"] = 0;

            return sb.ToString();
        }
    }
}