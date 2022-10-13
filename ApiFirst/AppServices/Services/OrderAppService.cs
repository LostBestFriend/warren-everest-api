using AppModels.AppModels.Order;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderServices;
        private readonly IMapper _mapper;

        public OrderAppService(IMapper mapper, IOrderService orderServices)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _orderServices = orderServices ??
                throw new ArgumentNullException(nameof(orderServices));
        }

        public Task<long> CreateAsync(CreateOrder model)
        {
            Order order = _mapper.Map<Order>(model);
            return _orderServices.CreateAsync(order);
        }

        public IEnumerable<OrderResponse> GetAll()
        {
            var result = _orderServices.GetAll();
            return _mapper.Map<IList<OrderResponse>>(result);
        }

        public async Task<OrderResponse> GetByIdAsync(long id)
        {
            var result = await _orderServices.GetByIdAsync(id);
            return _mapper.Map<OrderResponse>(result);
        }

        public int GetQuotesAvaliable(long portfolioId, long productId)
        {
            return _orderServices.GetQuotesAvaliable(portfolioId, productId);
        }

        public IEnumerable<OrderResponse> GetExecutableOrders()
        {
            var orders = _orderServices.GetExecutableOrders();

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);

        }

        public void Update(long id, UpdateOrder model)
        {
            Order order = _mapper.Map<Order>(model);
            _orderServices.Update(order);
        }

        public void Delete(long id)
        {
            _orderServices.Delete(id);
        }
    }
}
