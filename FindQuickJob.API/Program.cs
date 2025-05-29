using Microsoft.IdentityModel.Tokens;
using FindQuickJob.Application.Mappings;
using FindQuickJob.Application.Services;
using FindQuickJob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

//Bu kisim CQRS uyumu icin gerekli servisleri ekler
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); //For Minimal API#
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FindQuickJob API",
        Version = "v1",
        Description = "Minimal bir baþlangýç API yapýsý"
    });
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer("Bearer", options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
       };
   });
builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindQuickJob API V1");
    });
}

app.UseHttpsRedirection();
// CORS ayarlarýný uyguluyor
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "FindQuickJob API is running!");
app.Run();

