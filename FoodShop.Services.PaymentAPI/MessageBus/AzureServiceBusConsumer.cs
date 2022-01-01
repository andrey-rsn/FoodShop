
using Azure.Messaging.ServiceBus;
using FoodShop.Services.OrderAPI.Messaging;
using FoodShop.Services.PaymentAPI.Messages;
using FoodShop.Services.PaymentAPI.Models;

using FoodShop.Services.PaymentAPI.PaymentProcess;

using Newtonsoft.Json;
using System.Text;

namespace FoodShop.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer:IAzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _subscriptionPayment;
        private readonly string _orderPaymentProcessTopic;
        //private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;        
        private readonly IMessageBus _messageBus;
        private readonly IProcessPayment _processPayment;
        private ServiceBusProcessor _orderPaymentProcessor;
        private string _paymentUpdateTopic;

        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IProcessPayment processPayment)
        {
            _configuration = configuration;
            _messageBus = messageBus;
            _processPayment = processPayment;
            _serviceBusConnectionString= _configuration.GetConnectionString("AzureBus");
            _subscriptionPayment= _configuration.GetConnectionString("OrderPaymentProcessSubscription");
            _orderPaymentProcessTopic= _configuration.GetConnectionString("OrderPaymentTopic");
            _paymentUpdateTopic = _configuration.GetConnectionString("updatePaymentTopic");
            var client = new ServiceBusClient(_serviceBusConnectionString);
            _orderPaymentProcessor = client.CreateProcessor(_orderPaymentProcessTopic, _subscriptionPayment);
        }




        public async Task Start()
        {
            _orderPaymentProcessor.ProcessMessageAsync += ProcessPayments;
            _orderPaymentProcessor.ProcessErrorAsync += ErrorHandler;
            await _orderPaymentProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _orderPaymentProcessor.StopProcessingAsync();
            await _orderPaymentProcessor.DisposeAsync();
        }
        Task ErrorHandler (ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResult = new()
            { 
                Status=result, 
                OrderId=paymentRequestMessage.OrderId
            };


            

            try
            {
                await _messageBus.PublishMessage(updatePaymentResult, _paymentUpdateTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
