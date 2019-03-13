using ISApp.Core.Services;
using ISApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISApp.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/patients")]
    public class PatientsController : BaseController
    {
        #region Declarations and Constructors

        private readonly IPatientService _patientService;
        private readonly ISecurityService _securityService;
        private readonly IPrescriptionService _prescriptionService;

        public PatientsController(
            IPatientService patientService,
            ISecurityService securityService,
            IPrescriptionService prescriptionService)
        {
            this._patientService = patientService;
            this._securityService = securityService;
            this._prescriptionService = prescriptionService;
        }

        #endregion Declarations and Constructors

        #region Public Methods

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = _patientService.GetAllPatients();
            return Ok(list);
        }

        [HttpGet]
        [Route("{patientId:int}")]
        public IHttpActionResult GetPatient(int patientId)
        {          
            var patient = _patientService.GetPatient(patientId);
            return Ok(patient);         
        }

        [HttpPost]
        public IHttpActionResult SavePatient(PatientDto patient)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid && patient != null)
            {
                var patientId = _patientService.SavePatient(patient);

                if (patientId > 0)
                {
                    patient.PatientId = patientId;
                    return Ok(patient);
                }
                else
                {
                    return Error("Unable to save patient record.");
                }
            }
            else if (patient == null)
            {
                return Error("No values submitted.");
            }

            return JSONValidationErrors();
        }

        [HttpPut]
        public IHttpActionResult UpdatePatient(PatientDto patient)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid && patient != null)
            {
                var isSuccess = _patientService.UpdatePatient(patient);

                if (isSuccess)
                {
                    return Ok(patient);
                }
                else
                {
                    return Error("Unable to update patient record.");
                }
            }
            else if (patient == null)
            {
                return Error("No values submitted.");
            }

            return JSONValidationErrors();
        }

        [HttpDelete]
        [Route("{patientId:int}")]
        public IHttpActionResult DeletePatient(int patientId)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            var isSuccess = _patientService.DeletePatient(patientId);

            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return Error("Unable to delete patient record.");
            }
        }

        [HttpGet]
        [Route("prescriptions/{patientId:int}")]
        public IHttpActionResult GetPatientPrescription(int patientId)
        {
            var list = _prescriptionService.GetPatientPrescription(patientId);
            return Ok(list);
        }
        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods
    }
}
