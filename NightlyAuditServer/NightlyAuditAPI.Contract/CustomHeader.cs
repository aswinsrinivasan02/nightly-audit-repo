using BallyTech.Infrastructure.Security;
using BallyTech.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace NightlyAuditAPI.Contract
{
    public class CustomHeader : IEndpointBehavior, IDispatchMessageInspector
    {
        private const string NameSpace = "http://www.BallyTech.com/Headers";
        private const string SessionId = "SessionId";

        private readonly APISecurityProvider provider = DIContainer.Instance.Get<APISecurityProvider>();

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            IResponse response = this.provider.Validate(new HeaderProvider(request));
            if (response.Result != ResultType.Success)
            {
                throw new FaultException<IResponse>(response);
            }

            return null;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
            ExecutionContextProvider.Clear();
        }

        private class HeaderProvider : IAPIRequestHeaderProvider
        {
            private readonly Header header;

            public HeaderProvider(Message request)
            {
                this.header = new Header(request);
            }

            public IAPIRequestHeader Get()
            {
                return this.header;
            }
        }

        private class Header : IAPIRequestHeader
        {
            private readonly string apiName;
            private readonly string sessionId;

            public Header(Message request)
            {
                this.apiName = request.Headers.Action;
                this.sessionId = request.Headers.GetHeader<string>(CustomHeader.SessionId, CustomHeader.NameSpace);
            }

            public string APIName
            {
                get { return this.apiName; }
            }

            public string SessionId
            {
                get { return this.sessionId; }
            }
        }
    }
}
