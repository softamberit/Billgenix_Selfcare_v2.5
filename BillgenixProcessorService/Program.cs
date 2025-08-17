using BillgenixProcessorService.Models;
using BillgenixProcessorService.Processor.LiveTraffic;
using BillgenixProcessorService.ScheduleJob;
using CrystalQuartz.AspNetCore;
using Quartz;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((context, loggerConfiguration) =>
//{
//    loggerConfiguration.WriteTo.Console();
//    loggerConfiguration.ReadFrom.Configuration(context.Configuration);

//});
builder.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger, Microsoft.Extensions.Logging.Logger<ErrorDetails>>();

builder.Services.ConfigureOptions<ConfigurationCustomerLiveTraffic>();

//builder.Services.AddHttpClient("DefaultHttpClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7133/"); // Set your base address here
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.AddSingleton(appSettings);

builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(); // enable SignalR

builder.Services.AddCors(options =>
{

    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:15365")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .Build()
        );
});


var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

//app.UseSerilogRequestLogging();
app.UseRouting();
//app.MapControllerRoute("default", "hangfire");

IScheduler schedules = await QuartzScheduleJob.GetSchedulers();

app.UseCrystalQuartz(() => schedules);
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<CustomerTrafficHub>("/trafficHub"); // Map Hub, for .net6 map it above end point with app.MapHub<>

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "hangfire");
});
app.Run();
