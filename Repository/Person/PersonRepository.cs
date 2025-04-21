using WoundClinic.Data;
using WoundClinic.Repository.IRepository;

namespace WoundClinic.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;
        public PersonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Person Create(Person person)
        {
            _db.Persons.Add(person);
            _db.SaveChanges();
            return person;
        }

        public Person Update(Person person)
        {
            _db.Persons.Update(person);
            _db.SaveChanges();
            return person;
        }
        public bool Delete(Person person)
        {
            _db.Persons.Remove(person);
            _db.SaveChanges();
            return true;

        }

        public Person Get(long id)
        {
            var person = _db.Persons.FirstOrDefault(x => x.NationalCode == id);
            if (person == null)
            {
                return new Person();
            }
            return person;    
        }

        public IEnumerable<Person> GetAll()
        {
            return _db.Persons.ToList();
        }

    }
}
