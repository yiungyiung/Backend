using Azure;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Security.Cryptography;
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
    public DbSet<Framework> Framework { get; set; }
    public DbSet<Domain> Domain { get; set; }
    public DbSet<QuestionFramework> QuestionFramework { get; set; }

    public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<QuestionQuestionnaire> QuestionQuestionnaire { get; set; }
    public DbSet<Status> Status { get; set; }

    public DbSet<QuestionnaireAssignment> QuestionnaireAssignments { get; set; }
    public DbSet<Responses> Responses { get; set; }
    public DbSet<OptionResponses> OptionResponses { get; set; }
    public DbSet<TextBoxResponses> TextBoxResponses { get; set; }

    public DbSet<FileUploadResponses> FileUploadResponses { get; set; }
    public DbSet<FileUpload> FileUploads { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FrameworkDetails> FrameworkDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileUpload>()
            .HasKey(fu => fu.FileUploadID);  // Primary key

        modelBuilder.Entity<FileUpload>()
            .HasOne(fu => fu.Question)  // Foreign key to Question
            .WithMany()
            .HasForeignKey(fu => fu.QuestionID)
            .OnDelete(DeleteBehavior.Cascade);  // Delete behavior

        modelBuilder.Entity<FileUploadResponses>()
            .HasKey(fur => fur.FileUploadResponseID);  // Primary key

        modelBuilder.Entity<FileUploadResponses>()
            .HasOne(fur => fur.Response)  // Foreign key to Responses
            .WithMany()
            .HasForeignKey(fur => fur.ResponseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FileUploadResponses>()
            .HasOne(fur => fur.FileUpload)  // Foreign key to FileUpload
            .WithMany()
            .HasForeignKey(fur => fur.FileUploadID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<VendorHierarchy>()
            .HasKey(vh => vh.HierarchyID);

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

        modelBuilder.Entity<QuestionQuestionnaire>()
    .HasKey(qq => qq.QuestionQuestionnaireID);

        modelBuilder.Entity<QuestionQuestionnaire>()
            .HasOne(qq => qq.Question)
            .WithMany() 
            .HasForeignKey(qq => qq.QuestionID)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<QuestionQuestionnaire>()
            .HasOne(qq => qq.Questionnaire)
            .WithMany() 
            .HasForeignKey(qq => qq.QuestionnaireID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuestionnaireAssignment>()
        .HasKey(qa => qa.AssignmentID);

        modelBuilder.Entity<QuestionnaireAssignment>()
            .HasOne(qa => qa.Vendor)
            .WithMany()
            .HasForeignKey(qa => qa.VendorID);

        modelBuilder.Entity<QuestionnaireAssignment>()
            .HasOne(qa => qa.Questionnaire)
            .WithMany()
            .HasForeignKey(qa => qa.QuestionnaireID);

        modelBuilder.Entity<QuestionnaireAssignment>()
            .HasOne(qa => qa.Status)
            .WithMany()
            .HasForeignKey(qa => qa.StatusID);

        modelBuilder.Entity<Responses>()
    .HasKey(r => r.ResponseID);

        modelBuilder.Entity<Responses>()
            .HasOne(r => r.Assignment)
            .WithMany()
            .HasForeignKey(r => r.AssignmentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Responses>()
            .HasOne(r => r.Question)
            .WithMany()
            .HasForeignKey(r => r.QuestionID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OptionResponses>()
            .HasKey(or => or.OptionResponseID);

        modelBuilder.Entity<OptionResponses>()
            .HasOne(or => or.Response)
            .WithMany()
            .HasForeignKey(or => or.ResponseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OptionResponses>()
            .HasOne(or => or.Option)
            .WithMany()
            .HasForeignKey(or => or.OptionID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TextBoxResponses>()
            .HasKey(tr => tr.TextBoxResponseID);

        modelBuilder.Entity<TextBoxResponses>()
            .HasOne(tr => tr.Response)
            .WithMany()
            .HasForeignKey(tr => tr.ResponseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TextBoxResponses>()
            .HasOne(tr => tr.TextBox)
            .WithMany()
            .HasForeignKey(tr => tr.TextBoxID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<FrameworkDetails>()
            .HasKey(fd => fd.FrameworkID);  // FrameworkID is the primary key of FrameworkDetails
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
       .Property(u => u.IsActive)
       .IsRequired()
       .HasDefaultValue(1);
        modelBuilder.Entity<FrameworkDetails>()
            .HasOne(fd => fd.Framework)  // FrameworkDetails has a one-way relationship with Framework
            .WithMany()                  // No navigation property in Framework
            .HasForeignKey(fd => fd.FrameworkID)  // Foreign key in FrameworkDetails
            .OnDelete(DeleteBehavior.Cascade);    // Optional: configure delete behavior
        base.OnModelCreating(modelBuilder);
    }

    public void SeedData()
    {
        // Only seed if the database is empty
        if (!Roles.Any() && !Category.Any() && !Tier.Any() && !Domain.Any() && !Framework.Any() && !FrameworkDetails.Any() && !Status.Any() && !Users.Any())
        {
            // Seed Roles
            Roles.AddRange(
                new Role { RoleName = "Admin" },
                new Role { RoleName = "Manager" },
                new Role { RoleName = "Analyst" },
                new Role { RoleName = "Vendor" }
            );

            // Seed Category
            Category.AddRange(
                new Category { CategoryName = "Critical" },
                new Category { CategoryName = "Non-Critical" },
                new Category { CategoryName = "Others" }
            );

            // Seed Tier
            Tier.AddRange(
                new Tier { TierName = "Tier 1" },
                new Tier { TierName = "Tier 2" },
                new Tier { TierName = "Tier 3" }
            );

            // Seed Domain
            Domain.AddRange(
                new Domain { DomainName = "General" },
                new Domain { DomainName = "Registration/ Certification/Policies" },
                new Domain { DomainName = "Financial Information" },
                new Domain { DomainName = "Human Resource Development" },
                new Domain { DomainName = "Corporate Social Responsibility" },
                new Domain { DomainName = "Safety Health & Environment" },
                new Domain { DomainName = "Supply Chain" },
                new Domain { DomainName = "Complaint/ Notices/Penalty" },
                new Domain { DomainName = "Grievance Mechanism" },
                new Domain { DomainName = "Declaration" }
            );

            // Seed Framework
            Framework.AddRange(
                new Framework { FrameworkName = "BRSR" },
                new Framework { FrameworkName = "UNGP" },
                new Framework { FrameworkName = "OECD" },
                new Framework { FrameworkName = "EU-NFRD" },
                new Framework { FrameworkName = "Germany-SCDDA" }
            );

            // Save changes to generate IDs for Framework
            SaveChanges();
        }
        if (!FrameworkDetails.Any() && !Status.Any())
        {
            // Seed FrameworkDetails
            FrameworkDetails.AddRange(
                new FrameworkDetails { FrameworkID = Framework.Single(f => f.FrameworkName == "BRSR").FrameworkID, Details = "BRSR (Business Responsibility and Sustainability Reporting) is a reporting framework designed for Indian companies to report on their ESG performance.", Link = "https://www.example.com/brsr" },
                new FrameworkDetails { FrameworkID = Framework.Single(f => f.FrameworkName == "UNGP").FrameworkID, Details = "UNGP (United Nations Guiding Principles on Business and Human Rights) provides guidelines for States and companies to prevent, address and remedy human rights abuses.", Link = "https://www.example.com/ungp" },
                new FrameworkDetails { FrameworkID = Framework.Single(f => f.FrameworkName == "OECD").FrameworkID, Details = "OECD (Organisation for Economic Co-operation and Development) Guidelines are recommendations for responsible business conduct.", Link = "https://www.example.com/oecd" },
                new FrameworkDetails { FrameworkID = Framework.Single(f => f.FrameworkName == "EU-NFRD").FrameworkID, Details = "EU-NFRD (Non-Financial Reporting Directive) requires large companies to disclose information on the way they operate and manage social and environmental challenges.", Link = "https://www.example.com/eu-nfrd" },
                new FrameworkDetails { FrameworkID = Framework.Single(f => f.FrameworkName == "Germany-SCDDA").FrameworkID, Details = "Germany-SCDDA (Supply Chain Due Diligence Act) is a law requiring German companies to address human rights risks in their supply chains.", Link = "https://www.example.com/germany-scdda" }
            );

            // Seed Status
            Status.AddRange(
                new Status {StatusName = "Complied" },
                new Status {StatusName = "Non Complied" }
            );
            SaveChanges();
        }
        if(!UnitOfMeasurement.Any())
        {
            UnitOfMeasurement.AddRange(new UnitOfMeasurement { UOMType = "NA" });
            SaveChanges();
        }    
        if (!Users.Any())
        {
            string adminPassword = "admin";
            string hashedPassword = HashPassword(adminPassword);
            int adminRoleId = Roles.Single(r => r.RoleName == "Admin").RoleId;

            Database.ExecuteSqlRaw(
                "INSERT INTO Users (Name, Email, PasswordHash, Contact_Number, RoleId, IsActive) " +
                "VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                "Admin", "admin@admin.com", hashedPassword, "1234567890", adminRoleId, true);
        }

    }

    // Password hashing function
    private string HashPassword(string password)
    {

        using (var sha256 = SHA256.Create())

        {
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
