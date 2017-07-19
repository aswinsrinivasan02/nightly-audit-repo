using BallyTech.Infrastructure.Hosting.RESTHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BallyTech.UI.Web.Framework;

namespace BallyTech.UI.Web.Platform.WebService
{
    internal class HttpClientHandler : IHttpClientHandler
    {
        private const string SessionId = "SessionId";

        void IHttpClientHandler.AfterReceiveReply(HttpResponseMessage responseMessage)
        {
        }

        void IHttpClientHandler.BeforeSendMessage(HttpRequestMessage requestMessage)
        {
            BallyTech.Infrastructure.Security.ISession session = SessionStore.Get<BallyTech.Infrastructure.Security.ISession>("SessionInfo"); 
//            BallyTech.Infrastructure.Security.ISession session = SessionContext.Current;
            if (null != session)
            {
                requestMessage.Headers.Add(SessionId, session.ID);
            }
            else
            {
                requestMessage.Headers.Add(SessionId, string.Empty);
            }
        }
    }
}