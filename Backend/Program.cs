using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Model;
using Backend.Services;
using Backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddScoped<IDataService, DataService>();
services.AddScoped<IAdminService, AdminService>();
services.AddScoped<IVendorService, VendorService>();
services.AddScoped<IVendorHierarchy, VendorHierarchyService>();
services.AddScoped<IQuestionService, QuestionService>();
services.AddScoped<IQuestionnaireService, QuestionnaireService>();
services.AddScoped<IQuestionnaireAssignmentService, QuestionnaireAssignmentService>();
services.AddScoped<IEntityService, EntityService>();
services.AddScoped<IResponseService, ResponseService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<ComplianceService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<INotificationService, NotificationService>();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Apply pending migrations
    context.SeedData(); // Seed your initial data here
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
