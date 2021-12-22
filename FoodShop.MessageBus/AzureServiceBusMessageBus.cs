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

            var shortNames = json["result"]
                .Select(token => token["AzureBus"].Value<string>()).FirstOrDefault();

        }
        public Task PublishMessage(BaseMessage message, string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
