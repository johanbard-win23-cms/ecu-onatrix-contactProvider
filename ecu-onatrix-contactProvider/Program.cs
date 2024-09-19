using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ecu_onatrix_contactProvider.Data.Contexts;
using Infrastructure.Services;
using EmailSender;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContextFactory<DataContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("ONATRIX_DB"));
        });

        services.AddScoped<IContactRequestService, ContactRequestService>();
        services.AddScoped<IEmailSender, EmailSender.EmailSender>();

    })
    .Build();

host.Run();