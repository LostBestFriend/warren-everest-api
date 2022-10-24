using AppModels.AppModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(long id);
        Task<long> CreateAsync(CreateProduct model);
        void Update(long id, UpdateProduct model);
        void Delete(long id);
    }
}
