using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SunShine.Model;
using SunShine.EF;

namespace SunShine.BLL
{
    public class ProductService
    {
        public static List<Product> GetALL()
        {
            TN db = new TN();
            return db.Products.OrderBy(en => en.sortno).ToList();
        }

        public static List<ProductViewModel> GetALLViewModels() {
            TN db = new TN();
            return ConvertToViewModel(db.Products.OrderBy(en => en.sortno).ToList());
        }

        private static List<ProductViewModel> ConvertToViewModel(List<Product> products) {
            List<ProductViewModel> entities = new List<ProductViewModel>();
            TN db = new TN();
            List<ProductCategory> categories = db.ProductCategories.ToList();
            entities = products.Select(model => {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.CopyFromBase(model);
                ProductCategory category = categories.Find(en => model.idcategory == en.idcategory);
                viewModel.categoryName = category != null ? category.categoryname : "";
                viewModel.ParentCategory = category;
                return viewModel;
            }).ToList();
            return entities;
        }

        public static List<ProductViewModel> SearchViewModels(string name = "", string idcategory = "") {
            List<Product> list = Search(name, idcategory);
            return ConvertToViewModel(list);
        }

        public static List<Product> Search(string name="",string idcategory = "") {
            List<Product> list = new List<Product>();
            TN db = new TN();
            list=db.Products.Where(en=> 
            (string.IsNullOrEmpty(idcategory) ||en.idcategory == idcategory) &&
            (string.IsNullOrEmpty(name) || en.name.Contains(name)
            || en.basicinfo.Contains(name)||en.description.Contains(name)
            )
            ).ToList();
            return list;
        }

        public static Product Get(string idproduct)
        {
            TN db = new TN();
            return db.Products.Where(en => en.idproduct == idproduct).FirstOrDefault();
        }
        public static ProductViewModel GetViewModel(string idproduct) {
            TN db = new TN();
            return ConvertToViewModel( db.Products.Where(en => en.idproduct == idproduct).ToList()).First();
        }

        public static List<ProductViewModel> GetViewModelsByCategoryCode(string categoryCode) {
            TN db = new TN();
            ProductCategory category = ProductCategoryService.GetByCategoryCode(categoryCode);
            return GetViewModelsByCategory(category.idcategory);
        }
        public static List<ProductViewModel> GetViewModelsByCategory(string idcategory) {
            TN db = new TN();
            return ConvertToViewModel(db.Products.Where(en => en.idcategory == idcategory).ToList());
        }

        public static Product Edit(Product product)
        {
            TN db = new TN();
            Product oldProduct = db.Products.Where(en => en.idproduct == product.idproduct).FirstOrDefault();

            oldProduct.idproduct = product.idproduct;
            oldProduct.name = product.name;
            oldProduct.basicinfo = product.basicinfo;
            oldProduct.img = product.img;
            oldProduct.idcategory = product.idcategory;
            oldProduct.description = product.description;
            oldProduct.sortno = product.sortno;
            oldProduct.inuse = product.inuse;
            oldProduct.ishot = product.ishot;
            oldProduct.isnew = product.isnew;
            oldProduct.seotitle = product.seotitle;
            oldProduct.seokeyword = product.seokeyword;
            oldProduct.seodescription = product.seodescription;
            db.SaveChanges();
            return oldProduct;
        }



        public static Product Add(Product product)
        {
            TN db = new TN();
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public static int MaxProductImageSort(ModuleType moduleType,string idproduct)
        {
            TN db = new TN();
            return db.Products.Where(en => en.idproduct == idproduct).Max(en => en.sortno) ?? 0;
        }


        public static bool SetDefaultImage(string idproduct)
        {
            bool result = false;
            try
            {
                TN db = new TN();
                Picture firstImage = db.Pictures.Where(en => en.idmodule == idproduct&&en.moduletype==(int)ModuleType.Product).OrderBy(en => en.sortno).First();
                string imagPath = firstImage == null ? "" : firstImage.path;
                Product product = db.Products.Find(idproduct);
                product.img = imagPath;
                db.SaveChanges();

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static bool Delete(List<string> idproducts)
        {
            int count = 0;
            TN db = new TN();
            for (int i = 0; i < idproducts.Count; i++)
            {
                Product product = db.Products.Remove(db.Products.Find(idproducts[i]));
                if (product != null)
                {
                    count++;
                }
            }
            db.SaveChanges();
            return count > 0 ? true : false;
        }
    }
}
