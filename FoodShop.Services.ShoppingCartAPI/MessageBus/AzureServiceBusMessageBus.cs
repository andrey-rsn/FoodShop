using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FoodShop.Services.ShoppingCartAPI.MessageBus
{

        public class AzureServiceBusMessageBus : IMessageBus
        {

            public async Task PublishMessage(BaseMessage message, string topicName)
            {
                ISenderClient senderClient = new TopicClient(SD.AzureBusConnection, topicName);

                var JsonMessage = JsonConvert.SerializeObject(message);
                var finalMessage = new Message(Encoding.UTF8.GetBytes(JsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString()
                };
                await senderClient.SendAsync(finalMessage);
                await senderClient.CloseAsync();
            }
        }
    
}
