using WoundClinic.Models;

namespace WoundClinic.Services.IRepository
{
    public interface IPersonRepository
    {
        public Task<Person> CreateAsync(Person person);

        public Task<Person> UpdateAsync(Person person);

        public Task<bool> DeleteAsync(Person person);

        public Task<Person> GetAsync(long id);

        public Task<bool> CheckPersonExist(long id);
        
        public Task<IEnumerable<Person>> GetAllAsync();

    }
}
