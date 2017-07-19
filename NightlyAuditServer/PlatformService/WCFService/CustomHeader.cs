using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BallyTech.Infrastructure.Security;
using BallyTech.UI.Web.Framework;
    
namespace BallyTech.UI.Web.Platform.WebService
{
    public class CustomHeader : IEndpointBehavior, IClientMessageInspector
    {
        private const string SessionId = "SessionId";
        private const string NameSpace = "http://www.BallyTech.com/Headers"; 
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
           
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            BallyTech.Infrastructure.Security.ISession session = SessionStore.Get<BallyTech.Infrastructure.Security.ISession>("SessionInfo");
            //BallyTech.Infrastructure.Security.ISession session = SessionContext.Current;
            if (null != session)
            {
                request.Headers.Add(MessageHeader.CreateHeader(SessionId, NameSpace, session.ID));
            }
            else
            {
                request.Headers.Add(MessageHeader.CreateHeader(SessionId, NameSpace, string.Empty));
            }

            return null;
        }
    }
}
