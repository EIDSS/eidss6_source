using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eidss.model.Enums;
using eidss.openapi.bll.Bll;
using eidss.openapi.contract;
using eidss.openapi.bll.Exceptions;
using eidss.openapi.web.Utils;

namespace eidss.openapi.web.Controllers
{
    /// <summary>Ehealth REST service for EIDSS Open API</summary>
    [EidssFilterAction]
    public class EhealthController : ApiController
    {
        /// <summary>Create Human Case(s) transferred from e-Health</summary>
        /// <remarks>HTTP POST verb<br/>
        /// http://hostname/api/Ehealth/CreateCase [value in the body] <br/>
        /// http://hostname/api/json/Ehealth/CreateCase [value in the body] <br/>
        /// http://hostname/api/xml/Ehealth/CreateCase [value in the body] <br/>
        /// </remarks>
        /// <param name="request">e-Health data to transfer</param>
        /// <returns>Response consisting of the result code and description</returns>
        /// <example><code>
        /// (POST) http://eidss.hostname.org/api/Ehealth/CreateCase [data in the body] 
        /// (POST) http://eidss.hostname.org/api/json/Ehealth/CreateCase [data in the body] 
        /// (POST) http://eidss.hostname.org/api/xml/Ehealth/CreateCase [data in the body] 
        /// </code></example>
        /// <example><code>
        /// // C#
        /// // TODO
        /// </code></example>
        /// <exception cref="OpenApiErrorCode.A0001">User login can't be empty</exception>
        /// <exception cref="OpenApiErrorCode.A0002">User with such login/password is not found</exception>
        /// <exception cref="OpenApiErrorCode.A0003">The database version is absent or in incorrect format</exception>
        /// <exception cref="OpenApiErrorCode.A0004">Login is locked</exception>
        /// <exception cref="OpenApiErrorCode.A0005">Password is expired</exception>
        /// <exception cref="OpenApiErrorCode.A0006">Language is not supported</exception>
        /// <exception cref="OpenApiErrorCode.A0007">External system is not supported</exception>
        /// <exception cref="OpenApiErrorCode.A0008">Login is failed (general login error)</exception>
        /// <exception cref="OpenApiErrorCode.A0009">Login is required</exception>
        /// <exception cref="OpenApiErrorCode.A0010">Access is denied</exception>
        /// <exception cref="OpenApiErrorCode.M0001">Validation of the model is failed</exception>
        [System.Web.Http.HttpPost]
        [EidssBasicAuthorize(InsertPermission = EIDSSPermissionObject.HumanCase)]
        [ModelValidation]
        public EHealthCaseResponse CreateCase([FromBody] EHealthCaseRequest request)
        {
            EHealthCaseResponse response = null;
            try
            {
               EHealthCaseErrorCode checkCode;
               string strOutcomeComment = string.Empty;
               response = EHealthCaseRequestBll.CreateCase(request, out checkCode, out strOutcomeComment);
               EHealthCaseRequestBll.RaiseErrorIfNeeded(checkCode, strOutcomeComment);
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }
    }
}
