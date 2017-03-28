namespace SunShine.EF {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TN : DbContext {
        public TN()
            : base("name=TN") {
        }

        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<FriendURL> FriendURLs { get; set; }
        public virtual DbSet<ManageUser> ManageUsers { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<SEO> SEOs { get; set; }
        public virtual DbSet<SiteCategory> SiteCategories { get; set; }
        public virtual DbSet<WebSiteInfo> WebSiteInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Advertise>()
                .Property(e => e.idadvertise)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.idarticle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.introduction)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.seotitle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.seokeyword)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.seodescription)
                .IsUnicode(false);

            modelBuilder.Entity<FriendURL>()
                .Property(e => e.idurl)
                .IsUnicode(false);

            modelBuilder.Entity<FriendURL>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<FriendURL>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.iduser)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.md5salt)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<ManageUser>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.idimage)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.idmodule)
                .IsUnicode(false);

            modelBuilder.Entity<Picture>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.idproduct)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.basicinfo)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.seotitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.seokeyword)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.seodescription)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.categorycode)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.categoryname)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.keyword)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.idseo)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.seotitle)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.seokeywords)
                .IsUnicode(false);

            modelBuilder.Entity<SEO>()
                .Property(e => e.seodescription)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.idcategory)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.categorycode)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.categoryname)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.englishname)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.parentid)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.keyword)
                .IsUnicode(false);

            modelBuilder.Entity<SiteCategory>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.idsite)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.sitename)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.copyright)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.hotphoneallcountry)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.hotphone)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.qq)
                .IsUnicode(false);

            modelBuilder.Entity<WebSiteInfo>()
                .Property(e => e.qq2)
                .IsUnicode(false);
        }
    }
}
