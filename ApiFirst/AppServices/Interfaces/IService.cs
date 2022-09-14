using AppServices.DTOs;

namespace AppServices.Interfaces
{
    public interface IService<T> where T : BaseDTO
    {
        public int Create(T model);
        public int Delete(int id);
        public List<T> GetAll();
        public T GetSpecific(int id);
        public int Update(T model);
        public int Modify(T model);
    }
}
