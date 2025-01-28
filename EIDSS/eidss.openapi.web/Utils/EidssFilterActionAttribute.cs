using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using eidss.openapi.bll.Exceptions;
using log4net;

namespace eidss.openapi.web.Utils
{
    public class EidssFilterActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string rawRequest = string.Empty;
            string methodType = actionContext.Request.Method.Method;
            if (methodType.Equals("Post", StringComparison.InvariantCultureIgnoreCase) ||
                methodType.Equals("Put", StringComparison.InvariantCultureIgnoreCase)
               )
            {
                try
                {
                    using (var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result))
                    {
                        stream.BaseStream.Position = 0;
                        rawRequest = stream.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    rawRequest = string.Empty;
                }
            }

            string requestUri = actionContext.Request.RequestUri.OriginalString;
            string requestString = actionContext.Request.ToString();
            string args = actionContext.ActionArguments.Aggregate("", (r, i) => r += "[" + i.Key + ":" + i.Value + "]");
            if (string.IsNullOrEmpty(rawRequest))
            {
                rawRequest = args;
            }
            else if (!string.IsNullOrEmpty(args))
            {
                rawRequest = string.Format("{0}\r\n{1}", args, rawRequest);
            }

            Logger.Instance.LogStart(requestUri, methodType);
            Logger.Instance.LogInfo(requestUri, methodType, requestString, rawRequest);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                string errCode;
                if (actionExecutedContext.Exception is OpenApiException)
                {
                    errCode = (actionExecutedContext.Exception as OpenApiException).ErrorCode;
                }
                else
                {
                    errCode = OpenApiErrorCode.G0001.ToString();
                }
                var errMes = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response.ReasonPhrase = errCode;
                actionExecutedContext.Response.Headers.Add("ErrorCode", errCode);
                actionExecutedContext.Response.Headers.Add("ErrorMessage", errMes);
                Logger.Instance.LogError(actionExecutedContext.Request.RequestUri.OriginalString, actionExecutedContext.Request.Method.Method, errCode, actionExecutedContext.Exception);
            }
            Logger.Instance.LogInfoReturn(actionExecutedContext.Request.RequestUri.OriginalString, actionExecutedContext.Request.Method.Method,
                actionExecutedContext.Response.ToString(), actionExecutedContext.Response.Content == null ? "" : actionExecutedContext.Response.Content.ReadAsStringAsync().Result);
            Logger.Instance.LogFinish(actionExecutedContext.Request.RequestUri.OriginalString, actionExecutedContext.Request.Method.Method);
        }
    }
}