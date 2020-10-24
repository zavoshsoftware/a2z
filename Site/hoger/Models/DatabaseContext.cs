using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
             System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Models.ProductGroup> ProductGroups { get; set; }
        public DbSet<Models.Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public System.Data.Entity.DbSet<Models.Province> Provinces { get; set; }
        public System.Data.Entity.DbSet<Models.City> Cities { get; set; }
        public System.Data.Entity.DbSet<Models.DiscountCode> DiscountCodes { get; set; }
        public DbSet<ZarinpallAuthority> ZarinpallAuthorities { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Product_Color> ProductColors { get; set; }
        public DbSet<TextType> TextTypes { get; set; }
        public DbSet<Text> Texts { get; set; }
    }
}
