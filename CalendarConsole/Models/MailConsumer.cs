using MassTransit;
using Newtonsoft.Json;

namespace CalendarConsole.Models;

public class MailConsumer : IConsumer<MyMessage>
{

    public MailConsumer()
    {
        
    }
    
    public async Task Consume(ConsumeContext<MyMessage> context)
    {
        Console.WriteLine($"Received Text: {context.Message.RequestBody}");
    
        var jsonMail = JsonConvert.DeserializeObject<JsonMail.Root>(context.Message.RequestBody);

        // Affichage des données de la classe Value
        Console.WriteLine($"Subscription ID: {jsonMail.value[0].subscriptionId}");
        Console.WriteLine($"Subscription Expiration Date Time: {jsonMail.value[0].subscriptionExpirationDateTime}");
        Console.WriteLine($"Change Type: {jsonMail.value[0].changeType}");
        Console.WriteLine($"Resource: {jsonMail.value[0].resource}");
        Console.WriteLine($"Client State: {jsonMail.value[0].clientState}");
        Console.WriteLine($"Tenant ID: {jsonMail.value[0].tenantId}");

        // Affichage des données de la classe ResourceData
        Console.WriteLine($"Resource Type: {jsonMail.value[0].resourceData.odatatype}");
        Console.WriteLine($"Resource ID: {jsonMail.value[0].resourceData.odataid}");
        Console.WriteLine($"Resource ID: {jsonMail.value[0].resourceData.id}");

        await Task.CompletedTask;
    }
}