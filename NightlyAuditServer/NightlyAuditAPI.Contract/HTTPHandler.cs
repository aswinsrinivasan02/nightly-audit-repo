using BallyTech.Infrastructure.Hosting.RESTHttp;
using BallyTech.Infrastructure.Security;
using BallyTech.Infrastructure.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NightlyAuditAPI.Contract
{
    internal class HttpServerHandler : IHttpServerHandler
    {
        private const string APIName = "APIName";

        private const string SessionId = "SessionId";
        private readonly APISecurityProvider provider = DIContainer.Instance.Get<APISecurityProvider>();

        void IHttpServerHandler.BeforeSendReply(HttpResponseMessage responseMessage)
        {
            ExecutionContextProvider.Clear();
        }

        void IHttpServerHandler.OnReceiveRequest(HttpRequestMessage requestMessage)
        {
            IResponse response = this.provider.Validate(new HeaderProvider(requestMessage));
            if (response.Result != ResultType.Success)
            {
                throw new ModuleException(response.ErrorCodes[0], null, this.GetParamCollectiom(response.Params));
            }
        }

        private Pair<string, string>[] GetParamCollectiom(IDictionary dictionary)
        {
            Pair<string, string>[] retValue = new Pair<string, string>[dictionary.Count];
            for (int i = 0; i < dictionary.Count; i++)
            {
                retValue[i] = new Pair<string, string>(dictionary.Keys.OfType<object>().ElementAt(i).ToString(),
                    dictionary.Values.OfType<object>().ElementAt(i).ToString());
            }

            return retValue;
        }

        private class Header : IAPIRequestHeader
        {
            private readonly string apiName;

            private readonly string sessionId;

            public Header(HttpRequestMessage requestMessage)
            {
                this.apiName = this.GetValue(requestMessage.Headers, HttpServerHandler.APIName);
                this.sessionId = this.GetValue(requestMessage.Headers, HttpServerHandler.SessionId);
            }

            public string APIName
            {
                get { return this.apiName; }
            }

            public string SessionId
            {
                get { return this.sessionId; }
            }

            private string GetValue(HttpHeaders headers, string key)
            {
                string retValue = string.Empty;
                IEnumerable<string> result;
                if (headers.TryGetValues(key, out result) && null != result && result.Count() == 1)
                {
                    retValue = result.First();
                }

                return retValue;
            }
        }

        private class HeaderProvider : IAPIRequestHeaderProvider
        {
            private readonly Header header;

            public HeaderProvider(HttpRequestMessage request)
            {
                this.header = new Header(request);
            }

            public IAPIRequestHeader Get()
            {
                return this.header;
            }
        }
    }
}
