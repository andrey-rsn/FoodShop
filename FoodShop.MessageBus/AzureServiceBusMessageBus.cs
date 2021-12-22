using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string _AzureBusConnectionString;
        public AzureServiceBusMessageBus()
        {
            var text = File.ReadAllText("appsettings.json");
            var json = JObject.Parse(text);

            _AzureBusConnectionString = json["result"]
                .Select(token => token["AzureBus"].Value<string>()).FirstOrDefault();

        }
        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            ISenderClient senderClient = new TopicClient(_AzureBusConnectionString,topicName);

            var JsonMessage =JsonConvert.SerializeObject(message);
            var finalMessage = new Message(Encoding.UTF8.GetBytes(JsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };
            await senderClient.SendAsync(finalMessage);
            await senderClient.CloseAsync();
        }
    }
}
