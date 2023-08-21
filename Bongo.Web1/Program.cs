using Bongo.Core.Services.IServices;
using Bongo.Core.Services;
using Bongo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IStudyRoomService, StudyRoomService>();
builder.Services.AddScoped<IStudyRoomRepository, StudyRoomRepository>();
builder.Services.AddScoped<IStudyRoomBookingService, StudyRoomBookingService>();
builder.Services.AddScoped<IStudyRoomBookingRepository, StudyRoomBookingRepository>();

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
