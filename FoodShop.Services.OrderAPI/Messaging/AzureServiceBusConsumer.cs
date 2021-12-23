using AutoMapper;
using Azure.Messaging.ServiceBus;
using FoodShop.Services.OrderAPI.Models;
using FoodShop.Services.OrderAPI.Models.Dto;
using FoodShop.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace FoodShop.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _subscriptionName;
        private readonly string _checkoutMessageTopic;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private ServiceBusProcessor _checkOutProcessor;
        public AzureServiceBusConsumer(IOrderRepository orderRepository, IMapper mapper, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
            _serviceBusConnectionString = _configuration.GetConnectionString("AzureBus");
            _subscriptionName = _configuration.GetConnectionString("SubscriptionName");
            _checkoutMessageTopic = _configuration.GetConnectionString("checkoutmessagetopic");
            var client = new ServiceBusClient(_serviceBusConnectionString);
            _checkOutProcessor = client.CreateProcessor(_checkoutMessageTopic, _subscriptionName);
        }

        public async Task Start()
        {
            _checkOutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await _checkOutProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _checkOutProcessor.StopProcessingAsync();
            await _checkOutProcessor.DisposeAsync();
        }
        Task ErrorHandler (ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            CheckoutHeaderDTO checkoutHeaderDTO= JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            OrderHeader orderHeader = _mapper.Map<OrderHeader>(checkoutHeaderDTO);
            foreach(var details in checkoutHeaderDTO.CartDetails)
            {
                OrderDetails orderDetails= _mapper.Map<OrderDetails>(details);
                orderHeader.CartTotalItems += details.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(_mapper.Map<OrderHeaderDTO>(orderHeader));

        }
    }
}
