using GlassECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlassECommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackAttachment> FeedbackAttachments { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ModelAttachment> ModelAttachments { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CollectionProduct> CollectionProducts { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Color> Colors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<User>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<CollectionProduct>().HasKey(c => new { c.ProductId, c.CollectionId });
            SeedRole(builder);
            SeedUnit(builder);
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
              new IdentityRole() { Id = "1", Name = "CUSTOMER", ConcurrencyStamp = "1", NormalizedName = "CUSTOMER" },
              new IdentityRole() { Id = "2", Name = "ADMIN", ConcurrencyStamp = "2", NormalizedName = "ADMIN" }
            );
        }
        
        private void SeedUnit(ModelBuilder builder)
        {
            builder.Entity<Unit>().HasData(
                new Unit
                {
                    UnitId = 1,
                    UnitName = "Cái"
                },
               new Unit
               {
                   UnitId = 2,
                   UnitName = "Thùng"
               },
               new Unit
               {
                   UnitId = 3,
                   UnitName = "Viên"
               },
               new Unit
               {
                   UnitId = 4,
                   UnitName = "Mũi"
               }
            );
        }

    }
}
