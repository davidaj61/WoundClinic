using WoundClinic.Data;

namespace WoundClinic.Repository.IRepository
{
    public interface IPersonRepository
    {
        public Person Create(Person person);

        public Person Update(Person person);

        public Person Delete(Person person);

        public Person Get(long id);
        
        public IEnumerable<Person> GetAll();


    }
}
