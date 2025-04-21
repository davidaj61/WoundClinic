using WoundClinic.Data;

namespace WoundClinic.Repository.IRepository
{
    public interface IPatientRepository
    {
        public Task<Patient> CreateAsync(Patient patient);

        public Task<Patient> UpdateAsync(Patient patient);

        public Task<bool> DeleteAsync(Patient patient);

        public Task<Patient> GetAsync(long id);

        public Task<IEnumerable<Patient>> GetAllAsync();
    }
}
