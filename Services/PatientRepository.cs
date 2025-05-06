using Microsoft.EntityFrameworkCore;
using WoundClinic.Data;
using WoundClinic.Models;
using WoundClinic.Services.IRepository;

namespace WoundClinic.Services
{
    public class PatientRepository:IPatientRepository
    {
        private readonly ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await _db.Patients.AddAsync(patient);
            await _db.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            _db.Patients.Update(patient);
            await _db.SaveChangesAsync();
            return patient;
        }
        public async Task<bool> DeleteAsync(Patient patient)
        {
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<Patient> GetAsync(long id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(x => x.NationalCode == id);
            if (patient == null)
            {
                return new Patient();
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _db.Patients.ToListAsync();
        }
    }
}
