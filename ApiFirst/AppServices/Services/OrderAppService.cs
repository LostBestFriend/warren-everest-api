using AppModels.AppModels.Orders;
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

        public async Task<long> CreateAsync(CreateOrder model)
        {
            Order order = _mapper.Map<Order>(model);
            return await _orderServices.CreateAsync(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var result = await _orderServices.GetAllAsync();
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

        public async Task<IEnumerable<OrderResponse>> GetExecutableOrdersAsync()
        {
            var orders = await _orderServices.GetExecutableOrdersAsync();

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
