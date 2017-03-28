using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SunShine.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute(
            //    name: "product",
            //    url: "Product/List_{idproduct}_{idcategory}_{categoryCode}_{keyword}_{pageIndex}.html",
            //    defaults: new
            //    {
            //        controller = "Product",
            //        action = "List",
            //        idproduct = UrlParameter.Optional,
            //        idcategory = UrlParameter.Optional,
            //        categoryCode = UrlParameter.Optional,
            //        keyword = UrlParameter.Optional,
            //        pageIndex = UrlParameter.Optional
            //    }
            //);
            
            routes.MapRoute(
                name: "product_category_pages",
                url: "Product/Category/{idcategory}/{pageIndex}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_category",
                url: "Product/Category/{idcategory}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_categorycode_pages",
                url: "Product/Categories/{categoryCode}/{pageIndex}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_categorycode",
                url: "Product/Categories/{categoryCode}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_search_pages",
                url: "Product/Search/{keyword}/{pageIndex}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_search",
                url: "Product/Search/{keyword}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "product_list",
                url: "Product/List.html",
                defaults: new { controller = "Product", action = "List", idproduct = UrlParameter.Optional, idcategory = UrlParameter.Optional, categoryCode = UrlParameter.Optional, keyword = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "product_list_pages",
               url: "Product/List/{pageIndex}.html",
               defaults: new { controller = "Product", action = "List", idproduct = UrlParameter.Optional, idcategory = UrlParameter.Optional, categoryCode = UrlParameter.Optional, keyword = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "product_detail",
                url: "Product/Detail/{idproduct}.html",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: "case_category_current_pages",
                url: "Case/Category/{categoryCode}/{currentCategoryCode}/{pageIndex}.html",
                defaults: new { controller = "Case", action = "Index" }
            );

            routes.MapRoute(
                name: "case_category_current",
                url: "Case/Category/{categoryCode}/{currentCategoryCode}.html",
                defaults: new { controller = "Case", action = "Index" }
            );

            routes.MapRoute(
                name: "case_category_pages",
                url: "Case/Categories/{categoryCode}/{pageIndex}.html",
                defaults: new { controller = "Case", action = "Index" }
            );

            routes.MapRoute(
                name: "case_category",
                url: "Case/Categories/{categoryCode}.html",
                defaults: new { controller = "Case", action = "Index" }
            );

            routes.MapRoute(
                name: "case_detail",
                url: "Case/Detail/{idarticle}.html",
                defaults: new { controller = "Case", action = "Index" }
            );

            routes.MapRoute(
                name: "manage_default",
                url: "Manage/{action}",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Default_html",
                url: "{controller}/{action}.html",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

           

        }
    }
}
