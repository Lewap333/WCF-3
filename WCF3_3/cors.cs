using System;

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.ServiceModel;

namespace WCF3_3
{
    public class EnableCorsBehavior : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CorsMessageInspector());
            base.AddServerErrorHandlers(endpoint, endpointDispatcher);
        }
    }

    public class CorsMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (WebOperationContext.Current?.IncomingRequest.Method != "OPTIONS")
                return null;

            var response = WebOperationContext.Current.OutgoingResponse;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var response = WebOperationContext.Current?.OutgoingResponse;
            response?.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }

    public class EnableCorsBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                if (!endpoint.EndpointBehaviors.Contains(typeof(WebHttpBehavior)))
                    continue;

                endpoint.EndpointBehaviors.Remove(typeof(WebHttpBehavior));
                endpoint.EndpointBehaviors.Add(new EnableCorsBehavior());
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}