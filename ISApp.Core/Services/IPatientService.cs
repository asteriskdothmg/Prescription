using ISApp.Core.Entities;
using ISApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Core.Services
{
    public interface IPatientService: IBaseService<Patient>
    {
        int SavePatient(PatientDto patient);
        IQueryable<PatientDto> GetAllPatients();
        PatientDto GetPatient(int patientId);
        bool UpdatePatient(PatientDto patient);
        bool DeletePatient(int patientId);
    }
}
