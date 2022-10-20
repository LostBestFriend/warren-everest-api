using AppServices.Interfaces;
using AppServices.Services;
using AutoMapper;
using DomainServices.Interfaces;
using Moq;

namespace AppServices.Tests.Services
{
    public class PortfolioAppServiceTest
    {
        private readonly Mock<IPortfolioService> _portfolioService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ICustomerBankInfoAppService> _customerBankInfoAppService;
        private readonly Mock<IProductAppService> _productAppService;
        private readonly Mock<IOrderAppService> _orderAppService;
        private readonly Mock<IPortfolioProductService> _portfolioProductService;
        private readonly PortfolioAppService _portfolioAppService;

        public PortfolioAppServiceTest()
        {
            _portfolioService = new();
            _mapper = new();
            _customerBankInfoAppService = new();
            _productAppService = new();
            _orderAppService = new();
            _portfolioProductService = new();
            _portfolioAppService = new PortfolioAppService(portfolio: _portfolioService.Object, mapper: _mapper.Object, customerBankInfoAppServices: _customerBankInfoAppService.Object, productAppServices: _productAppService.Object, orderAppServices: _orderAppService.Object, portfolioProductServices: _portfolioProductService.Object);
        }
    }
}
