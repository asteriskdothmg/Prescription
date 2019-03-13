using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ISApp.Web.Models;
using ISApp.Core.Services;

namespace ISApp.Web.Controllers
{
    public class BaseController : ApiController
    {
        #region Public Methods

        protected IHttpActionResult Unauthorized(string message = "Unauthorized")
        {
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = message };
            throw new HttpResponseException(msg);
        }

        protected IHttpActionResult Success(string message = "", object data = null)
        {
            var result = new
            {
                success = true,
                message = String.IsNullOrEmpty(message) ? "Success." : message,
                data = data
            };

            return Ok(result);
        }

        protected IHttpActionResult Success(object data)
        {
            return Success("", data);
        }

        protected IHttpActionResult Error(string message = "", object data = null)
        {
            var result = new
            {
                success = false,
                message = String.IsNullOrEmpty(message) ? "Error processing the request." : message,
                data = data
            };

            return Ok(result);
        }

        protected IHttpActionResult Error(object data)
        {
            return Error("", data);
        }

        protected IHttpActionResult JSONValidationErrors()
        {
            List<string> Keys = new List<string>();
            JObject json = new JObject();

            json["IsValidationError"] = true;
            json["IsSuccess"] = false;

            var errors = ModelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage))
                    .ToList();

            if (errors.Count > 0)
            {
                var index = 0;

                foreach (var error in errors)
                {
                    index++;

                    var sep = Keys.Count == 0 ? string.Empty : ",";
                    var key = String.IsNullOrEmpty(error.Key) ? string.Format("Field{0}", index) : error.Key;
                    var value = String.IsNullOrEmpty(error.Value) ? "Error validating the data." : error.Value;

                    var keys = error.Key.Split('.');

                    if (keys.Length > 1)
                    {
                        key = string.Join(string.Empty, keys.Skip(1));
                    }

                    key = key.Replace("]", string.Empty).Replace("[", string.Empty);

                    if (!Keys.Contains(key))
                    {
                        Keys.Add(key);
                        json[key] = value;
                    }
                }
            }
            else
            {
                return Success();
            }

            return Content<object>(HttpStatusCode.BadRequest, json);
        }

        protected bool IsAuthorized(ISecurityService securityService)
        {
            var result = new AuthorizationResult();

            try
            {
                var authHeader = Request.Headers.Authorization;

                if (authHeader == null)
                {
                    result.IsSucess = false;
                    result.Message = "Missing Authorization";
                }
                else
                {
                    var authParams = Encoding.Default.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

                    if (authParams.Length != 2)
                    {
                        result.IsSucess = false;
                        result.Message = "Incomplete Authorization";
                    }
                    else
                    {
                        var appName = authParams[0];
                        var authKey = authParams[1];

                        if (String.IsNullOrEmpty(appName) || String.IsNullOrEmpty(authKey))
                        {
                            result.IsSucess = false;
                            result.Message = "Incomplete Authorization";
                        }
                        else
                        {
                            Guid guid;
                            var isGuid = Guid.TryParse(authKey.ToString(), out guid);

                            if (isGuid)
                            {
                                result.IsSucess = securityService.IsValidKey(guid, appName);
                                result.Message = result.IsSucess ? "Success" : "Invalid Key";
                            }
                            else
                            {
                                result.IsSucess = false;
                                result.Message = "Invalid Key";
                            }
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                result.IsSucess = false;
                result.Message = "An error occured";
            }

            return result.IsSucess;
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods

        #region Helpers

        #endregion Helpers

    }
}
