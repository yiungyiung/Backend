using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Tier> Tier { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<VendorHierarchy> vendorHierarchy { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Textbox> Textboxes { get; set; }
        public DbSet<Framework> Frameworks{ get; set; }
        public DbSet<QuestionFramework> QuestionFramework { get; set; }

        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorHierarchy>()
                .HasKey(vh => vh.HierarchyID); // Set primary key

            modelBuilder.Entity<VendorHierarchy>()
                .HasOne<Vendor>()
                .WithMany()
                .HasForeignKey(vh => vh.ParentVendorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendorHierarchy>()
                .HasOne<Vendor>()
                .WithMany()
                .HasForeignKey(vh => vh.ChildVendorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnitOfMeasurement>()
        .HasKey(u => u.UOMID);

            modelBuilder.Entity<Textbox>()
                .HasOne(t => t.UnitOfMeasurement)
                .WithMany()
                .HasForeignKey(t => t.UOMID)
                .OnDelete(DeleteBehavior.SetNull);
            base.OnModelCreating(modelBuilder);
          
        }
    }
}
