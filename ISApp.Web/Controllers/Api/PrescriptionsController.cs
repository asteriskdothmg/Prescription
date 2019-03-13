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
    [RoutePrefix("api/prescriptions")]
    public class PrescriptionsController : BaseController
    {
        #region Declarations and Constructors

        private readonly IPrescriptionService _prescriptionService;
        private readonly ISecurityService _securityService;

        public PrescriptionsController(
            IPrescriptionService prescriptionService,
            ISecurityService securityService)
        {
            this._prescriptionService = prescriptionService;
            this._securityService = securityService;
        }

        #endregion Declarations and Constructors

        #region Public Methods

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = _prescriptionService.GetAllPrescriptions();
            return Ok(list);
        }

        [HttpGet]
        [Route("{prescriptionId:int}")]
        public IHttpActionResult GetPrescription(int prescriptionId)
        {
            var patient = _prescriptionService.GetPrescription(prescriptionId);
            return Ok(patient);         
        }

        [HttpPost]
        public IHttpActionResult SavePatient(PrescriptionDto prescription)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid && prescription != null)
            {
                try
                {
                    var prescriptionId = _prescriptionService.SavePrescription(prescription);

                    if (prescriptionId > 0)
                    {
                        prescription = _prescriptionService.GetPrescription(prescriptionId);
                        return Ok(prescription);
                    }
                    else
                    {
                        return Error("Unable to save prescription record.");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("FK_Prescription_Patient"))
                    {
                        ModelState.AddModelError("PatientId", "Patient Id is not valid.");
                    }
                    else
                    {
                        return Error("Unable to save prescription record.");
                    }
                }
                
            }
            else if (prescription == null)
            {
                return Error("No values submitted.");
            }

            return JSONValidationErrors();
        }

        [HttpPut]
        public IHttpActionResult UpdatePrescription(PrescriptionDto prescription)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isSuccess = _prescriptionService.UpdatePrescription(prescription);

                    if (isSuccess)
                    {
                        prescription = _prescriptionService.GetPrescription(prescription.Id);
                        return Ok(prescription);
                    }
                    else
                    {
                        return Error("Unable to update prescription record.");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("FK_Prescription_Patient"))
                    {
                        ModelState.AddModelError("PatientId", "Patient Id is not valid.");
                    }
                    else
                    {
                        return Error("Unable to update prescription record.");
                    }
                }
                
            }
            else if (prescription == null)
            {
                return Error("No values submitted.");
            }

            return JSONValidationErrors();
        }

        [HttpDelete]
        [Route("{prescriptionId:int}")]
        public IHttpActionResult DeletePatient(int prescriptionId)
        {
            if (!IsAuthorized(_securityService))
            {
                return Unauthorized();
            }

            var isSuccess = _prescriptionService.DeletePrescription(prescriptionId);

            if (isSuccess)
            {
                return Ok();
            }
            else
            {
                return Error("Unable to delete prescription record.");
            }
        }

        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods
    }
}
