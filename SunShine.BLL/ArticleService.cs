using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShine.Model;
using SunShine.EF;

namespace SunShine.BLL
{
    public class ArticleService
    {
        public static List<Article> GetALL() {
            TN db = new TN();
            return db.Articles.OrderBy(en => en.sortno).ToList();
        }

        private static List<ArticleViewModel> ConvertToViewModel(List<Article> articles) {
            List<ArticleViewModel> entities = new List<ArticleViewModel>();
            TN db = new TN();
            List<SiteCategory> categories = db.SiteCategories.ToList();
            entities = articles.Select(model => {
                ArticleViewModel viewModel = new ArticleViewModel();
                viewModel.CopyFromBase(model);
                SiteCategory category = categories.Find(en => model.idcategory == en.idcategory);
                viewModel.Category = category;
                return viewModel;
            }).ToList();
            return entities;
        }

        public static List<ArticleViewModel> SearchViewModels(string title = "", string idcategory = "") {
            List<Article> list = Search(title, idcategory);
            return ConvertToViewModel(list);
        }

        public static List<Article> Search(string title = "", string idcategory = "") {
            List<Article> list = new List<Article>();
            TN db = new TN();
            list = db.Articles.Where(en =>
              (string.IsNullOrEmpty(idcategory) || en.idcategory == idcategory) &&
              (string.IsNullOrEmpty(title) || en.title.Contains(title))
            ).ToList();
            return list;
        }

        public static List<ArticleViewModel> GetArticlesByCategoryCode(string categoryCode) {
            List<ArticleViewModel> viewModels = new List<ArticleViewModel>();
            SiteCategory parentCategory =SiteCategoryService.GetByCode(categoryCode);
            TN db = new TN();
            List<Article> articles = db.Articles.Where(en => en.idcategory == parentCategory.idcategory).ToList();
            viewModels = ConvertToViewModel(articles);
            return viewModels;
        }

        public static Article Get(string idarticle) {
            TN db = new TN();
            return db.Articles.Where(en => en.idarticle == idarticle).FirstOrDefault();
        }

        public static ArticleViewModel GetViewModel(string idarticle)
        {
            TN db = new TN();
            return  ConvertToViewModel( db.Articles.Where(en => en.idarticle == idarticle).ToList()).FirstOrDefault();
        }

        public static Article Edit(Article article) {
            TN db = new TN();
            Article oldArticle = db.Articles.Where(en => en.idarticle == article.idarticle).FirstOrDefault();

            oldArticle.idarticle = article.idarticle;
            oldArticle.title = article.title;
            oldArticle.img = article.img;
            oldArticle.content = article.content;
            oldArticle.idcategory = article.idcategory;
            oldArticle.follow = article.follow;
            oldArticle.sortno = article.sortno;
            oldArticle.introduction = article.introduction;
            oldArticle.seotitle = article.seotitle;
            oldArticle.seokeyword = article.seokeyword;
            oldArticle.seodescription = article.seodescription;

            db.SaveChanges();
            return oldArticle;
        }



        public static Article Add(Article article) {
            TN db = new TN();
            db.Articles.Add(article);
            db.SaveChanges();
            return article;
        }


        public static bool Delete(List<string> idarticles) {
            int count = 0;
            TN db = new TN();
            for (int i = 0; i < idarticles.Count; i++) {
                Article article = db.Articles.Remove(db.Articles.Find(idarticles[i]));
                if (article != null) {
                    count++;
                }
            }
            db.SaveChanges();
            return count > 0 ? true : false;
        }


        public static bool SetDefaultImage(string idarticle) {
            bool result = false;
            try {
                TN db = new TN();
                Picture firstImage = db.Pictures.Where(en => en.idmodule == idarticle && en.moduletype == (int)ModuleType.Article).OrderBy(en => en.sortno).First();
                string imagPath = firstImage == null ? "" : firstImage.path;
                Article article = db.Articles.Find(idarticle);
                article.img = imagPath;
                db.SaveChanges();

            }
            catch (Exception) {
                result = false;
            }

            return result;
        }
    }
}
