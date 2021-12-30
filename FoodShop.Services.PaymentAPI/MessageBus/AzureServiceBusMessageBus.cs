using Azure.Messaging.ServiceBus;
using FoodShop.Services.PaymentAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace FoodShop.Services.OrderAPI.Messaging
{
    public class AzureServiceBusMessageBus:IMessageBus
    {
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(SD.AzureBusConnection);

            ServiceBusSender sender = client.CreateSender(topicName);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}
