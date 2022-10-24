using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GestionPrestamos2022;
using GestionPrestamos2022.DAL;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.BLL;
using Radzen.Blazor;
using Radzen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using GestionPrestamos2022.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

var ConStr = builder.Configuration.GetConnectionString("ConStr");

builder.Services.AddDbContext<Contexto>(Options =>
    Options.UseSqlite(ConStr)
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Contexto>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddScoped<OcupacionesBLL>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<PersonasBLL>();
builder.Services.AddScoped<PrestamosBLL>();
builder.Services.AddScoped<PagosBLL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
