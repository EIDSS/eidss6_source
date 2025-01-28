using eidss.openapi.bll.Exceptions;
using eidss.openapi.contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace eidss.openapi.web.Utils
{

    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                string errCode;
                if (actionExecutedContext.Exception is ModelValidationException)
                {
                    errCode = (actionExecutedContext.Exception as ModelValidationException).ErrorCode;
                }
                else
                {
                    errCode = OpenApiErrorCode.G0001.ToString();
                }
                var errMes = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response.ReasonPhrase = errCode;
                actionExecutedContext.Response.Headers.Add("ErrorCode", errCode);
                actionExecutedContext.Response.Headers.Add("ErrorMessage", errMes);
                var model = new EHealthCaseResponse
                {
                    Result = "Model Validation Exception",
                    Description = errMes
                };
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                actionExecutedContext.ActionContext.Response.Content = stringContent;

                Logger.Instance.LogError(actionExecutedContext.Request.RequestUri.OriginalString, actionExecutedContext.Request.Method.Method, errCode, actionExecutedContext.Exception);
            }
        }
    }
}