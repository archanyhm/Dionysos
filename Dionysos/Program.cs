using Dionysos.Data;
using Dionysos.Database;
using Dionysos.GrpcService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<MainDbContext>();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapGrpcService<PingpongService>();
app.MapGrpcService<ArticleCrudService>();
app.MapGrpcService<InventoryItemCrudService>();
app.MapGrpcService<VendorCrudService>();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();