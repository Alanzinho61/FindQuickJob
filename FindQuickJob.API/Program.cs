using FindQuickJob.Application.Mappings;
using FindQuickJob.Application.Services;
using FindQuickJob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); //For Minimal API
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
app.MapControllers();
app.MapGet("/", () => "FindQuickJob API is running!");
app.Run();

