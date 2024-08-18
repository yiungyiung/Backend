using Azure;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        base.OnModelCreating(modelBuilder);
    }
}
