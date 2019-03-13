using ISApp.Core.Data.UnitOfWorks;
using ISApp.Core.Entities;
using ISApp.Core.Services;
using ISApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Services
{
    public class PatientService : BaseService<Patient>, IPatientService
    {
        #region Declarations and Constructors

        public PatientService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion Declarations and Constructors

        #region Interface Implementations

        public int SavePatient(PatientDto patient)
        {
            var newPatient = new Patient()
            {
                LastName = patient.LastName,
                FirstName = patient.FirstName
            };

            var process = base.Insert(newPatient);

            return process.PatientId;
        }

        public IQueryable<PatientDto> GetAllPatients()
        {
            var list = (from pt in base.GetAllQueryable()
                        select new PatientDto()
                        {
                            PatientId = pt.PatientId,
                            LastName = pt.LastName,
                            FirstName = pt.FirstName
                        });

            return list;
        }

        public PatientDto GetPatient(int patientId)
        {
            return GetAllPatients().Where(p => p.PatientId == patientId).FirstOrDefault();
        }

        public bool UpdatePatient(PatientDto patient)
        {
            var existingPatient = base.GetAllQueryable().Where(p => p.PatientId == patient.PatientId).FirstOrDefault();

            if (existingPatient != null)
            {
                existingPatient.LastName = patient.LastName;
                existingPatient.FirstName = patient.FirstName;

                return base.Update(existingPatient);
            }

            return false;
        }


        public bool DeletePatient(int patientId)
        {
            return base.BulkDelete(p => p.PatientId == patientId);
        }

        #endregion Interface Implementations

        #region Private Methods

        #endregion Private Methods
    }
}
