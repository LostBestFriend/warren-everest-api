using DomainModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Task<Product> GetByIdAsync(long id);
        Task<long> CreateAsync(Product model);
        void Update(Product model);
        void Delete(long id);
    }
}
