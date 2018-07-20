using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

public class FaceB2BCustomEndpointBehavior : IEndpointBehavior
{
    public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
    { }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
    {
        clientRuntime.MessageInspectors.Add(
            new FaceB2BCustomMessageInspector()
            );
    }

    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
    { }

    public void Validate(ServiceEndpoint endpoint)
    { }
}

public class FaceB2BCustomMessageInspector : IClientMessageInspector, IDispatchMessageInspector
{

    #region IClientMessageInspector Members

    public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
    { }

    public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
    {
        int limit = request.Headers.Count;
        for (int i = 0; i < limit; ++i)
        {
            if (request.Headers[i].Name.Equals("VsDebuggerCausalityData"))
            {
                request.Headers.RemoveAt(i);
                break;
            }
        }

        return request;
    }
    #endregion

    #region IDispatchMessageInspector Members

    public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
    {
        return null;
    }

    public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
    { }

    #endregion
}
