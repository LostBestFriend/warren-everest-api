using DomainModels.Enums;
using DomainModels.Models;
using DomainServices.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork<WarrenContext> unitOfWork, IRepositoryFactory<WarrenContext> repository)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _repositoryFactory = repository ?? (IRepositoryFactory)_unitOfWork;
        }

        public async Task<long> CreateAsync(Order model)
        {
            var repository = _unitOfWork.Repository<Order>();

            await repository.AddAsync(model).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return model.Id;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery().Include(source => source.Include(order => order.Product).Include(order => order.Portfolio));

            return await repository.SearchAsync(query);
        }

        public async Task<Order> GetByIdAsync(long id)
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.SingleResultQuery()
                                   .AndFilter(order => order.Id == id)
                                   .Include(source => source.Include(order => order.Product).Include(order => order.Portfolio));

            var order = await repository.FirstOrDefaultAsync(query).ConfigureAwait(false);

            return order;
        }

        public async Task<IList<Order>> GetExecutableOrdersAsync()
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery().AndFilter(order => order.LiquidateAt.Date == DateTime.Now.Date);
            var orders = await repository.SearchAsync(query);

            return orders;
        }

        public void Update(Order model)
        {
            var repository = _unitOfWork.Repository<Order>();

            repository.Update(model);
            _unitOfWork.SaveChanges();
        }

        public async Task<int> GetQuotesAvaliableAsync(long portfolioId, long productId)
        {
            var repository = _repositoryFactory.Repository<Order>();
            var query = repository.MultipleResultQuery().AndFilter(order => order.ProductId == productId && order.PortfolioId == portfolioId);
            var allOrders = await repository.SearchAsync(query);

            if (!allOrders.Any())
                throw new ArgumentNullException($"Nenhuma cota disponível para o produto de Id: {productId} na carteira de Id {portfolioId}");

            int availableQuotes = 0;

            foreach (var order in allOrders)
            {
                availableQuotes = order.Direction == OrderDirection.Buy ?
                    availableQuotes += order.Quotes :
                    availableQuotes -= order.Quotes;
            }

            return availableQuotes;
        }
    }
}
