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
    public class PrescriptionService : BaseService<Prescription>, IPrescriptionService
    {
        #region Declarations and Constructors

        public PrescriptionService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion Declarations and Constructors

        #region Interface Implementations

        public int SavePrescription(PrescriptionDto prescription)
        {
            var newPrescription = new Prescription()
            {
                ExpirationDate = prescription.ExpirationDate,
                ProductName = prescription.ProductName,
                UsesLeft = prescription.UsesLeft,
                Description = prescription.Description,
                IsActive = prescription.IsActive,
                PatientId = prescription.PatientId
            };

            var process = base.Insert(newPrescription);

            return process.Id;

        }

        public IQueryable<PrescriptionDto> GetAllPrescriptions()
        {
            var list = (from ps in base.GetAllQueryable()
                        select new PrescriptionDto()
                        {
                            Id = ps.Id,
                            ExpirationDate = ps.ExpirationDate,
                            ProductName = ps.ProductName,
                            UsesLeft = ps.UsesLeft,
                            Description = ps.Description,
                            IsActive = ps.IsActive,
                            PatientId = ps.PatientId,
                            PatientName = ps.Patient.LastName + ", " + ps.Patient.FirstName
                        });

            return list;
        }

        public PrescriptionDto GetPrescription(int prescriptionId)
        {
            return GetAllPrescriptions().Where(p => p.Id == prescriptionId).FirstOrDefault();
        }

        public bool UpdatePrescription(PrescriptionDto prescription)
        {
            var existingPrescription = base.GetAllQueryable().Where(p => p.Id == prescription.Id).FirstOrDefault();

            if (existingPrescription != null)
            {
                existingPrescription.ExpirationDate = prescription.ExpirationDate;
                existingPrescription.ProductName = prescription.ProductName;
                existingPrescription.UsesLeft = prescription.UsesLeft;
                existingPrescription.Description = prescription.Description;
                existingPrescription.IsActive = prescription.IsActive;
                existingPrescription.PatientId = prescription.PatientId;

                return base.Update(existingPrescription);
            }

            return false;
        }

        public bool DeletePrescription(int prescriptionId)
        {
            return base.BulkDelete(p => p.Id == prescriptionId);
        }

        public IQueryable<PrescriptionDto> GetPatientPrescription(int patientId)
        {
            return GetAllPrescriptions().Where(p => p.PatientId == patientId);
        }

        #endregion Interface Implementations

        #region Private Methods

        #endregion Private Methods
    }
}
