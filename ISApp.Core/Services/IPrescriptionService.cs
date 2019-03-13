using ISApp.Core.Entities;
using ISApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Core.Services
{
    public interface IPrescriptionService : IBaseService<Prescription>
    {
        int SavePrescription(PrescriptionDto prescription);
        IQueryable<PrescriptionDto> GetAllPrescriptions();
        PrescriptionDto GetPrescription(int prescriptionId);
        bool UpdatePrescription(PrescriptionDto prescription);
        bool DeletePrescription(int prescriptionId);
        IQueryable<PrescriptionDto> GetPatientPrescription(int patientId);
    }
}

