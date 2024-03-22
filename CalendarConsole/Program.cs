// See https://aka.ms/new-console-template for more information

using CalendarConsole;
using Microsoft.Graph;

using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using CalendarConsole.Models;
using MassTransit;



using (var context = new CalendarContext())
{
    var identifiers = context.Identifiers.ToList();

    foreach (var identifier in identifiers)
    {
        Console.WriteLine($"ID: {identifier.Id}, Graph ID: {identifier.GraphId}, Company ID: {identifier.CompanyId}");
        
        var confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(identifier.ClientId)
            .WithClientSecret(identifier.SecretId) 
            .WithAuthority(new Uri($"https://login.microsoftonline.com/{identifier.TenantId}")) 
            .Build();

        Graph oGraph = new Graph(identifier.ClientId, identifier.SecretId, identifier.TenantId);
        
        var graphClient = oGraph.GetAuthenticatedGraphClient();
        var subscription = new Subscription
        {
            ChangeType = "created",
            NotificationUrl = " https://f90a-2a02-2788-1b8-3df-cda2-5b1e-3bf0-72cc.ngrok-free.app/api/HttpTrigger1",
            Resource = $"users/{identifier.GraphId}/mailFolders('Inbox')/messages",
            ExpirationDateTime = DateTimeOffset.UtcNow.AddMinutes(20) // Définissez une expiration pour l'abonnement
        };

        await graphClient.Subscriptions.Request().AddAsync(subscription);
    }
}


public static class ConsoleApp1
{
    private static IBusControl _bus;

    public static async Task Main()
    {
        Console.WriteLine("Console App is running.");

        var bus = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
        {
            var connectionString =
                "Endpoint=sb://oxy-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Nq5RvCUlqxO5sRZ1i86DRz7BYKLKAW+po+ASbId9GcA=";
            cfg.Host(connectionString);

            cfg.ReceiveEndpoint("mail-event",
                e => { e.Consumer<MailConsumer>(() => { return new MailConsumer(); }); });
        });

        _bus = bus;
        await _bus.StartAsync();

        while (true)
        {
            await Task.Delay(1000);
        }
    }
}