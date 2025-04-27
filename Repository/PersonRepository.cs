using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public async Task<Person> CreateAsync(Person person)
        {
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            _db.Persons.Update(person);
            await _db.SaveChangesAsync();
            return person;
        }
        public async Task<bool> DeleteAsync(Person person)
        {
            _db.Persons.Remove(person);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<Person> GetAsync(long id)
        {
            var person =await _db.Persons.FirstOrDefaultAsync(x => x.NationalCode == id);
            if (person == null)
            {
                return new Person();
            }
            return person;    
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return(await _db.Persons.ToListAsync());
        }

        public async Task<bool> CheckPersonExist(long id)
        {
            return (await _db.Persons.AnyAsync(x => x.NationalCode == id));
        }

        //create method for check person is exist

    }
}
