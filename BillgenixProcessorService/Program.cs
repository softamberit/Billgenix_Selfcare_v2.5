using Common.Infrastructure.Common;
using Common.Infrastructure.Models;
using Hangfire;
using Hangfire.Dashboard;
using Quartz;
using TrafficProcessorService.ApiIntegration;
using TrafficProcessorService.Extensions;
using TrafficProcessorService.Models;
using TrafficProcessorService.Processor.LiveTraffic;
using TrafficProcessorService.Repositories;

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
appSettings.ConnectionString_billgenix = builder.Configuration.GetConnectionString("Billgenix");
var AppSettingsService = new AppSettingsSevice
{
    baseUrlRadiusAPI = builder.Configuration.GetSection("AppSettings:baseUrlRadiusAPI").Value!,
    RunServerName = builder.Configuration.GetSection("AppSettings:runServerName").Value!

};


if (string.IsNullOrEmpty(AppSettingsService.RunServerName))
{
    throw new Exception("Please define a run server name and it will will be unique");
}
//appSettings.ConnectionString_billgenix = builder.Configuration.GetSection("AppSettings:baseUrlRadiusAPI");

var billgenixDb = new DbSettings
{
    ConnectionString = appSettings.ConnectionString_billgenix,
    DbServer = DbServer.MSSQL
};
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton(AppSettingsService);

builder.Services.AddKeyedSingleton<IConnectionFactory>("billgenix", new ConnectionFactory(billgenixDb));

builder.Services.AddHttpClient();
builder.Services.AddScoped<BillgenixRadiusClient>();
builder.Services.AddScoped<IBillgenixRepository, BillgenixRepository>();

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
        builder => builder.WithOrigins("http://localhost:15365", "https://myswift.amberit.com.bd")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .Build()
        );
});

//// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseFilter(new AutomaticRetryAttribute { Attempts = 0 })
    .UseInMemoryStorage());

//Add the processing server as IHostedService


builder.Services.AddHangfireServer();


var app = builder.Build();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

//app.UseSerilogRequestLogging();

//app.MapControllerRoute("default", "hangfire");

//IScheduler schedules = await QuartzScheduleJob.GetSchedulers();

//app.UseCrystalQuartz(() => schedules);

//var options = new DashboardOptions
//{
//    Authorization = new[] { new AuthorizationFilter { Users = "admin, superuser" ,Roles=""}, }, // Allow all users to access the dashboard
//};


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHangfireDashboard("/amber_hangfire", new DashboardOptions
    {
        IsReadOnlyFunc = (DashboardContext dashboardContext) =>
        {
            var context = dashboardContext.GetHttpContext();
            return !context.User.IsInRole("Admin");
        }
    });
    app.MapHangfireDashboard();

}


app.MapGet("/", () => "Welcome to Customer Usage traffic processor service!"); // Default endpoint

app.MapHub<CustomerTrafficHub>("/trafficHub"); // Map Hub, for .net6 map it above end point with app.MapHub<>
app.Run();
