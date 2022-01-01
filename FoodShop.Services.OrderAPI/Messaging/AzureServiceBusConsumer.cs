using AutoMapper;
using Azure.Messaging.ServiceBus;
using FoodShop.Services.OrderAPI.Models;
using FoodShop.Services.OrderAPI.Models.Dto;
using FoodShop.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace FoodShop.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer:IAzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionString;
        private readonly string _subscriptionName;
        private readonly string _checkoutMessageTopic;
        private readonly string _paymentUpdateResultTopic;
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        private readonly string _orderPaymentTopic;

        private ServiceBusProcessor _checkOutProcessor;
        private ServiceBusProcessor _updatePaymentStatusProcessor;
        public AzureServiceBusConsumer(OrderRepository orderRepository, IMapper mapper, IConfiguration configuration, IMessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
            _serviceBusConnectionString = _configuration.GetConnectionString("AzureBus");
            _subscriptionName = _configuration.GetConnectionString("SubscriptionName");
            _checkoutMessageTopic = _configuration.GetConnectionString("checkoutmessagetopic");
            _paymentUpdateResultTopic= _configuration.GetConnectionString("updatePaymentTopic");
            var client = new ServiceBusClient(_serviceBusConnectionString);
            _checkOutProcessor = client.CreateProcessor(_checkoutMessageTopic, _subscriptionName);
            _messageBus = messageBus;
            _orderPaymentTopic = configuration.GetConnectionString("OrderPaymentTopic");
            _updatePaymentStatusProcessor=client.CreateProcessor(_paymentUpdateResultTopic, _subscriptionName);
        }

        public async Task Start()
        {
            _checkOutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await _checkOutProcessor.StartProcessingAsync();

            _updatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            _updatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await _updatePaymentStatusProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await _checkOutProcessor.StopProcessingAsync();
            await _checkOutProcessor.DisposeAsync();

            await _updatePaymentStatusProcessor.StopProcessingAsync();
            await _updatePaymentStatusProcessor.DisposeAsync();
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
            CheckoutHeaderDTO checkoutHeaderDto= JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                EMail = checkoutHeaderDto.EMail,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDto.Phone,
                PickUpDateTime = checkoutHeaderDto.PickUpDateTime
            };
            foreach (var detailList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count
                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(_mapper.Map<OrderHeaderDTO>(orderHeader));
            PaymentRequestMessage paymentRequestMessage = new PaymentRequestMessage()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.EMail
            };

            try
            {
                await _messageBus.PublishMessage(paymentRequestMessage, _orderPaymentTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId,paymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);
        }
    }
}
