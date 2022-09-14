using DomainModels.Entities;

namespace DomainServices.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        int Create(T model);

        int Update(T model);

        int Delete(int id);

        List<T> GetAll();

        T GetSpecific(int id);

        int Modify(T model);

        Customer GetSpecificFromList(string cpf, string email);

        int DeleteFromList(string cpf, string email);
    }
}
