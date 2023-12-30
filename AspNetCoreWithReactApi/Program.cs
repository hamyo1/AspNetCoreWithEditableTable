using AspNetCoreWithReactApi.BackgroundTasks;
using AspNetCoreWithReactApi.Queuing;
using Lubinski.Commbox.API.BackgroundTasks;
using Microsoft.Extensions.Configuration;
using RazorPageTableProssesor.Interfaces;
using RazorPageTableProssesor.Services;
using Serilog;
using System.Net.Http.Headers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<ICreateClipsService, CreateClipsService>();

#region Queue
//Queue
builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
#endregion
builder.Services.AddScoped<ICustomerConversation, CustomerConversation>();

//where you use the quee   await _queue.QueueBackgroundWorkItem(eventArgs);
/*
 * inject example
         readonly IBackgroundTaskQueue _queue;
        readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IBackgroundTaskQueue queue, ILogger<ActivitiesController> logger)
        {
            _queue = queue;
            _logger = logger;
        }
 */

//adding serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

//DoNotCallMeCheckScheduler
builder.Services.AddHostedService<SomeScheduler>();
//builder.Services.AddSingleton<someService>();   
//builder.Services.AddSingleton<ISomeService>(serviceProvider => serviceProvider.GetRequiredService<SomeService>());


// http request
builder.Services.AddHttpClient("GetToken", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetSection("secrion")["raw"]);
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    c.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler { ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, sslErrors) => true };
});
builder.Services.AddHttpClient("someApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetSection("section")["raw"]);
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
